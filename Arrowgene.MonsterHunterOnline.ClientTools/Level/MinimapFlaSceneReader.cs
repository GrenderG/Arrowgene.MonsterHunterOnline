using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using Arrowgene.MonsterHunterOnline.ClientTools.Flash;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

public readonly record struct LevelClientMiniMapTransform(float A, float B, float C, float D, float Tx, float Ty)
{
    public static LevelClientMiniMapTransform Identity => new(1f, 0f, 0f, 1f, 0f, 0f);

    public LevelClientMiniMapTransform Append(LevelClientMiniMapTransform child)
    {
        return new LevelClientMiniMapTransform(
            (A * child.A) + (C * child.B),
            (B * child.A) + (D * child.B),
            (A * child.C) + (C * child.D),
            (B * child.C) + (D * child.D),
            (A * child.Tx) + (C * child.Ty) + Tx,
            (B * child.Tx) + (D * child.Ty) + Ty);
    }

    public Vec2 TransformPoint(float x, float y)
    {
        return new Vec2(
            (A * x) + (C * y) + Tx,
            (B * x) + (D * y) + Ty);
    }
}

public sealed class LevelClientMiniMapBitmapPlacement
{
    public string LibraryItemName { get; set; } = string.Empty;
    public string ArchiveEntryName { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = [];
    public float Width { get; set; }
    public float Height { get; set; }
    public LevelClientMiniMapTransform Transform { get; set; } = LevelClientMiniMapTransform.Identity;
    /// <summary>Region ID extracted from the FLA instance name (e.g. "r0" → 0). Null for non-region bitmaps.</summary>
    public int? RegionId { get; set; }
}

public sealed class LevelClientMiniMapMarker
{
    public string LibraryItemName { get; set; } = string.Empty;
    public string MarkerClass { get; set; } = string.Empty;
    public string MarkerType { get; set; } = string.Empty;
    public string Tooltip { get; set; } = string.Empty;
    public Vec2 Position2D { get; set; }

    public string DisplayText
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Tooltip) && !string.IsNullOrWhiteSpace(MarkerType))
            {
                return $"{Tooltip} ({MarkerType})";
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                return Tooltip;
            }

            if (!string.IsNullOrWhiteSpace(MarkerType))
            {
                return MarkerType;
            }

            return LibraryItemName;
        }
    }
}

internal sealed class MinimapFlaScene
{
    /// <summary>All bitmap placements from visible layers, in FLA draw order (bottom layer first).</summary>
    public List<LevelClientMiniMapBitmapPlacement> BitmapPlacements { get; } = [];
    public List<LevelClientMiniMapMarker> Markers { get; } = [];
}

internal static class MinimapFlaSceneReader
{
    public static MinimapFlaScene Read(FlaArchive archive, XDocument domDocument)
    {
        return new Parser(archive, domDocument).Read();
    }

    private sealed class Parser
    {
        private readonly FlaArchive _archive;
        private readonly XDocument _domDocument;
        private readonly Dictionary<string, MediaItem> _mediaItems;
        private readonly Dictionary<string, XElement?> _libraryItemCache;
        private readonly Dictionary<string, byte[]> _entryCache;

        public Parser(FlaArchive archive, XDocument domDocument)
        {
            _archive = archive;
            _domDocument = domDocument;
            _mediaItems = BuildMediaItems(domDocument);
            _libraryItemCache = new Dictionary<string, XElement?>(StringComparer.OrdinalIgnoreCase);
            _entryCache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
        }

        public MinimapFlaScene Read()
        {
            MinimapFlaScene scene = new();
            XElement? root = _domDocument.Root;
            if (root == null)
            {
                return scene;
            }

            // Collect visible-layer placements in FLA draw order.
            // If no visible layers have bitmaps, fall back to all layers.
            List<LevelClientMiniMapBitmapPlacement> allPlacements = [];
            List<LevelClientMiniMapBitmapPlacement> visiblePlacements = [];
            ParseTimelineOwner(root, LevelClientMiniMapTransform.Identity,
                allPlacements, visiblePlacements, scene.Markers, depth: 0, parentLayerVisible: true, regionId: null);

            List<LevelClientMiniMapBitmapPlacement> placements =
                visiblePlacements.Count > 0 ? visiblePlacements : allPlacements;
            foreach (LevelClientMiniMapBitmapPlacement placement in placements)
            {
                scene.BitmapPlacements.Add(placement);
            }

            return scene;
        }

        private void ParseTimelineOwner(XElement timelineOwner,
            LevelClientMiniMapTransform transform,
            List<LevelClientMiniMapBitmapPlacement> allPlacements,
            List<LevelClientMiniMapBitmapPlacement> visiblePlacements,
            List<LevelClientMiniMapMarker> markers,
            int depth,
            bool parentLayerVisible,
            int? regionId)
        {
            if (depth > 12)
            {
                return;
            }

            XElement? timeline = FindFirstDescendant(timelineOwner, "DOMTimeline");
            if (timeline == null)
            {
                return;
            }

            List<XElement> layers = FindChildren(FindFirstChild(timeline, "layers"), "DOMLayer").ToList();
            for (int layerIndex = layers.Count - 1; layerIndex >= 0; layerIndex--)
            {
                XElement layer = layers[layerIndex];

                // Guide/mask layers and hidden layers are Flash IDE authoring aids.
                // They are not rendered by the Flash player, so bitmaps from these layers
                // should only be considered for background detection, not as overlays.
                string? layerType = layer.Attribute("layerType")?.Value;
                bool isGuideOrMask = layerType is "guide" or "mask";
                bool isHidden = string.Equals(layer.Attribute("visible")?.Value, "false", StringComparison.OrdinalIgnoreCase);
                bool layerVisible = parentLayerVisible && !isGuideOrMask && !isHidden;

                XElement? frames = FindFirstChild(layer, "frames");
                XElement? frame = FindChildren(frames, "DOMFrame").FirstOrDefault();
                XElement? elements = FindFirstChild(frame, "elements");
                if (elements == null)
                {
                    continue;
                }

                foreach (XElement element in elements.Elements())
                {
                    ParseElement(element, transform, allPlacements, visiblePlacements, markers, depth, layerVisible, regionId);
                }
            }
        }

        private void ParseElement(XElement element,
            LevelClientMiniMapTransform parentTransform,
            List<LevelClientMiniMapBitmapPlacement> allPlacements,
            List<LevelClientMiniMapBitmapPlacement> visiblePlacements,
            List<LevelClientMiniMapMarker> markers,
            int depth,
            bool layerVisible,
            int? regionId)
        {
            string elementName = element.Name.LocalName;
            LevelClientMiniMapTransform elementTransform = parentTransform.Append(ParseMatrix(element));

            switch (elementName)
            {
                case "DOMBitmapInstance":
                    LevelClientMiniMapBitmapPlacement? placement = CreateBitmapPlacement(element, elementTransform);
                    if (placement != null)
                    {
                        placement.RegionId = regionId;
                        allPlacements.Add(placement);
                        if (layerVisible)
                        {
                            visiblePlacements.Add(placement);
                        }
                    }
                    break;

                case "DOMSymbolInstance":
                {
                    int? childRegionId = regionId ?? TryParseRegionId(element.Attribute("name")?.Value);
                    ParseLibraryItemTimeline(element.Attribute("libraryItemName")?.Value, elementTransform,
                        allPlacements, visiblePlacements, markers, depth + 1, layerVisible, childRegionId);
                    break;
                }

                case "DOMComponentInstance":
                    LevelClientMiniMapMarker? marker = CreateMarker(element, elementTransform);
                    if (marker != null)
                    {
                        markers.Add(marker);
                    }

                    ParseLibraryItemTimeline(element.Attribute("libraryItemName")?.Value, elementTransform,
                        allPlacements, visiblePlacements, markers, depth + 1, layerVisible, regionId);
                    break;
            }
        }

        private void ParseLibraryItemTimeline(string? libraryItemName,
            LevelClientMiniMapTransform transform,
            List<LevelClientMiniMapBitmapPlacement> allPlacements,
            List<LevelClientMiniMapBitmapPlacement> visiblePlacements,
            List<LevelClientMiniMapMarker> markers,
            int depth,
            bool layerVisible,
            int? regionId)
        {
            if (string.IsNullOrWhiteSpace(libraryItemName))
            {
                return;
            }

            XElement? libraryRoot = LoadLibraryItemRoot(libraryItemName);
            if (libraryRoot == null)
            {
                return;
            }

            ParseTimelineOwner(libraryRoot, transform, allPlacements, visiblePlacements, markers, depth, layerVisible, regionId);
        }

        private LevelClientMiniMapBitmapPlacement? CreateBitmapPlacement(XElement element, LevelClientMiniMapTransform transform)
        {
            string? libraryItemName = element.Attribute("libraryItemName")?.Value;
            if (string.IsNullOrWhiteSpace(libraryItemName))
            {
                return null;
            }

            // Resolve via media item href first (DOMBitmapItem name can differ from href),
            // then fall back to constructing the path from the library item name directly.
            string entryName;
            if (_mediaItems.TryGetValue(libraryItemName, out MediaItem mediaItem) &&
                !string.IsNullOrWhiteSpace(mediaItem.EntryName))
            {
                entryName = mediaItem.EntryName;
            }
            else
            {
                entryName = NormalizeArchivePath($"LIBRARY/{libraryItemName}");
            }

            byte[]? imageData = TryReadArchiveEntry(entryName);

            // When the LIBRARY/ PNG is missing or empty, fall back to Flash's
            // internal binary bitmap data (bin/*.dat referenced by bitmapDataHRef).
            if ((imageData == null || imageData.Length == 0) &&
                mediaItem?.BitmapDataEntryName != null)
            {
                byte[]? datBytes = TryReadArchiveEntry(mediaItem.BitmapDataEntryName);
                if (datBytes != null)
                {
                    imageData = TryDecodeFlashBitmapData(datBytes);
                }
            }

            if (imageData == null || imageData.Length == 0)
            {
                return null;
            }

            (float width, float height) = GetBitmapDimensions(libraryItemName, imageData);
            return new LevelClientMiniMapBitmapPlacement
            {
                LibraryItemName = libraryItemName,
                ArchiveEntryName = entryName,
                ImageData = imageData,
                Width = width,
                Height = height,
                Transform = transform,
            };
        }

        private LevelClientMiniMapMarker? CreateMarker(XElement element, LevelClientMiniMapTransform transform)
        {
            Dictionary<string, string> parameters = ParseInspectableParameters(element);
            if (parameters.Count == 0)
            {
                return null;
            }

            parameters.TryGetValue("m_class", out string? markerClass);
            parameters.TryGetValue("m_type", out string? markerType);
            parameters.TryGetValue("m_tips", out string? tooltip);

            return new LevelClientMiniMapMarker
            {
                LibraryItemName = element.Attribute("libraryItemName")?.Value ?? string.Empty,
                MarkerClass = markerClass ?? string.Empty,
                MarkerType = markerType ?? string.Empty,
                Tooltip = tooltip ?? string.Empty,
                Position2D = transform.TransformPoint(0f, 0f),
            };
        }

        private XElement? LoadLibraryItemRoot(string libraryItemName)
        {
            if (_libraryItemCache.TryGetValue(libraryItemName, out XElement? root))
            {
                return root;
            }

            string entryName = NormalizeArchivePath($"LIBRARY/{libraryItemName}.xml");
            if (!_archive.TryGetEntry(entryName, out FlaArchiveEntry? entry) || entry == null)
            {
                _libraryItemCache[libraryItemName] = null;
                return null;
            }

            string xml = _archive.ReadText(entryName);
            root = XDocument.Parse(xml).Root;
            _libraryItemCache[libraryItemName] = root;
            return root;
        }

        private byte[]? TryReadArchiveEntry(string entryName)
        {
            if (_entryCache.TryGetValue(entryName, out byte[]? data))
            {
                return data;
            }

            if (!_archive.TryGetEntry(entryName, out FlaArchiveEntry? entry) || entry == null)
            {
                return null;
            }

            data = _archive.Extract(entry);
            _entryCache[entryName] = data;
            return data;
        }

        private (float width, float height) GetBitmapDimensions(string libraryItemName, byte[] imageData)
        {
            if (_mediaItems.TryGetValue(libraryItemName, out MediaItem mediaItem))
            {
                if (mediaItem.Width > 0 && mediaItem.Height > 0)
                {
                    return (mediaItem.Width, mediaItem.Height);
                }
            }

            if (TryReadPngSize(imageData, out int width, out int height))
            {
                return (width, height);
            }

            return (0f, 0f);
        }



        private static Dictionary<string, MediaItem> BuildMediaItems(XDocument domDocument)
        {
            Dictionary<string, MediaItem> items = new(StringComparer.OrdinalIgnoreCase);
            XElement? root = domDocument.Root;
            if (root == null)
            {
                return items;
            }

            foreach (XElement bitmapItem in root.Descendants().Where(element => element.Name.LocalName == "DOMBitmapItem"))
            {
                string? name = bitmapItem.Attribute("name")?.Value;
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                float width = ParseFloat(bitmapItem.Attribute("frameRight")?.Value) / 20f;
                float height = ParseFloat(bitmapItem.Attribute("frameBottom")?.Value) / 20f;
                string? bitmapDataHRef = bitmapItem.Attribute("bitmapDataHRef")?.Value;
                string? bitmapDataEntry = !string.IsNullOrWhiteSpace(bitmapDataHRef)
                    ? NormalizeArchivePath($"bin/{bitmapDataHRef}")
                    : null;
                items[name] = new MediaItem(name, NormalizeArchivePath($"LIBRARY/{bitmapItem.Attribute("href")?.Value ?? name}"), width, height, bitmapDataEntry);
            }

            return items;
        }

        private static Dictionary<string, string> ParseInspectableParameters(XElement element)
        {
            Dictionary<string, string> values = new(StringComparer.OrdinalIgnoreCase);
            XElement? parametersElement = FindFirstChild(element, "parametersAsXML");
            if (parametersElement == null)
            {
                return values;
            }

            string raw = parametersElement.Value;
            if (string.IsNullOrWhiteSpace(raw))
            {
                return values;
            }

            try
            {
                XDocument doc = XDocument.Parse($"<parameters>{raw}</parameters>");
                foreach (XElement property in doc.Root?.Elements("property") ?? [])
                {
                    string? id = property.Attribute("id")?.Value;
                    XElement? inspectable = property.Descendants().FirstOrDefault(descendant => descendant.Name.LocalName == "Inspectable");
                    string? value = inspectable?.Attribute("defaultValue")?.Value;
                    if (!string.IsNullOrWhiteSpace(id) && value != null)
                    {
                        values[id] = value;
                    }
                }
            }
            catch
            {
                return values;
            }

            return values;
        }

        private static XElement? FindFirstDescendant(XElement? parent, string localName)
        {
            return parent?.Descendants().FirstOrDefault(descendant => descendant.Name.LocalName == localName);
        }

        private static XElement? FindFirstChild(XElement? parent, string localName)
        {
            return parent?.Elements().FirstOrDefault(child => child.Name.LocalName == localName);
        }

        private static IEnumerable<XElement> FindChildren(XElement? parent, string localName)
        {
            return parent?.Elements().Where(child => child.Name.LocalName == localName) ?? [];
        }

        private static LevelClientMiniMapTransform ParseMatrix(XElement element)
        {
            XElement? matrix = FindFirstChild(FindFirstChild(element, "matrix"), "Matrix");
            if (matrix == null)
            {
                return LevelClientMiniMapTransform.Identity;
            }

            return new LevelClientMiniMapTransform(
                GetFloat(matrix, "a", 1f),
                GetFloat(matrix, "b", 0f),
                GetFloat(matrix, "c", 0f),
                GetFloat(matrix, "d", 1f),
                GetFloat(matrix, "tx", 0f),
                GetFloat(matrix, "ty", 0f));
        }

        private static float GetFloat(XElement element, string attributeName, float defaultValue)
        {
            string? value = element.Attribute(attributeName)?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return ParseFloat(value);
        }

        private static float ParseFloat(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0f;
            }

            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        private static bool TryReadPngSize(byte[] data, out int width, out int height)
        {
            ReadOnlySpan<byte> signature = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
            if (data.Length < 24 || !data.AsSpan(0, signature.Length).SequenceEqual(signature))
            {
                width = 0;
                height = 0;
                return false;
            }

            width = (data[16] << 24) | (data[17] << 16) | (data[18] << 8) | data[19];
            height = (data[20] << 24) | (data[21] << 16) | (data[22] << 8) | data[23];
            return width > 0 && height > 0;
        }

        /// <summary>
        /// Decodes Flash CS5's internal binary bitmap format (bin/*.dat in FLA archives)
        /// into a PNG byte array. Format: 32-byte header (format, stride, width, height, ...)
        /// followed by raw-deflate compressed premultiplied ARGB pixel data.
        /// </summary>
        private static byte[]? TryDecodeFlashBitmapData(byte[] dat)
        {
            if (dat.Length < 36)
            {
                return null;
            }

            ushort width = BinaryPrimitives.ReadUInt16LittleEndian(dat.AsSpan(4));
            ushort height = BinaryPrimitives.ReadUInt16LittleEndian(dat.AsSpan(6));
            if (width == 0 || height == 0 || width > 8192 || height > 8192)
            {
                return null;
            }

            // Pixel data starts at offset 32, compressed with raw deflate.
            byte[] argbPixels;
            try
            {
                using MemoryStream compressedStream = new(dat, 32, dat.Length - 32);
                using DeflateStream deflate = new(compressedStream, CompressionMode.Decompress);
                using MemoryStream output = new(width * height * 4);
                deflate.CopyTo(output);
                argbPixels = output.ToArray();
            }
            catch
            {
                return null;
            }

            int stride = width * 4;
            int expectedBytes = stride * height;
            int availableRows = Math.Min(height, argbPixels.Length / stride);
            if (availableRows <= 0)
            {
                return null;
            }

            // Encode as PNG: convert premultiplied ARGB → standard RGBA.
            return EncodePng(width, availableRows, argbPixels, stride);
        }

        private static byte[] EncodePng(int width, int height, byte[] argbPixels, int srcStride)
        {
            // Build unfiltered PNG image data (filter byte 0 per row + RGBA pixels).
            int rowBytes = 1 + width * 4;
            byte[] raw = new byte[rowBytes * height];
            for (int y = 0; y < height; y++)
            {
                int dstRow = y * rowBytes;
                raw[dstRow] = 0; // filter: none
                int srcRow = y * srcStride;
                for (int x = 0; x < width; x++)
                {
                    int srcOff = srcRow + x * 4;
                    int dstOff = dstRow + 1 + x * 4;
                    byte a = argbPixels[srcOff];
                    byte r = argbPixels[srcOff + 1];
                    byte g = argbPixels[srcOff + 2];
                    byte b = argbPixels[srcOff + 3];

                    // Un-premultiply
                    if (a > 0 && a < 255)
                    {
                        r = (byte)Math.Min(255, r * 255 / a);
                        g = (byte)Math.Min(255, g * 255 / a);
                        b = (byte)Math.Min(255, b * 255 / a);
                    }

                    raw[dstOff] = r;
                    raw[dstOff + 1] = g;
                    raw[dstOff + 2] = b;
                    raw[dstOff + 3] = a;
                }
            }

            byte[] compressedPixels;
            using (MemoryStream ms = new())
            {
                using (ZLibStream zlib = new(ms, CompressionLevel.Fastest, true))
                {
                    zlib.Write(raw);
                }

                compressedPixels = ms.ToArray();
            }

            // Minimal PNG: signature + IHDR + IDAT + IEND
            using MemoryStream png = new();
            png.Write([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]);

            byte[] ihdr = new byte[13];
            BinaryPrimitives.WriteInt32BigEndian(ihdr.AsSpan(0), width);
            BinaryPrimitives.WriteInt32BigEndian(ihdr.AsSpan(4), height);
            ihdr[8] = 8;  // bit depth
            ihdr[9] = 6;  // color type: RGBA
            WritePngChunk(png, "IHDR"u8, ihdr);
            WritePngChunk(png, "IDAT"u8, compressedPixels);
            WritePngChunk(png, "IEND"u8, []);

            return png.ToArray();
        }

        private static void WritePngChunk(Stream output, ReadOnlySpan<byte> type, ReadOnlySpan<byte> data)
        {
            Span<byte> buf = stackalloc byte[4];
            BinaryPrimitives.WriteInt32BigEndian(buf, data.Length);
            output.Write(buf);
            output.Write(type);
            output.Write(data);

            // CRC32 over type + data (using System.IO.Hashing)
            uint crc = 0xFFFFFFFF;
            foreach (byte b in type)
            {
                crc = CrcTable[(crc ^ b) & 0xFF] ^ (crc >> 8);
            }

            foreach (byte b in data)
            {
                crc = CrcTable[(crc ^ b) & 0xFF] ^ (crc >> 8);
            }

            crc ^= 0xFFFFFFFF;
            BinaryPrimitives.WriteUInt32BigEndian(buf, crc);
            output.Write(buf);
        }

        private static readonly uint[] CrcTable = BuildCrcTable();

        private static uint[] BuildCrcTable()
        {
            uint[] table = new uint[256];
            for (uint n = 0; n < 256; n++)
            {
                uint c = n;
                for (int k = 0; k < 8; k++)
                {
                    c = (c & 1) != 0 ? 0xEDB88320 ^ (c >> 1) : c >> 1;
                }

                table[n] = c;
            }

            return table;
        }

        private static int? TryParseRegionId(string? instanceName)
        {
            if (instanceName == null || instanceName.Length < 2 || instanceName[0] != 'r')
            {
                return null;
            }

            return int.TryParse(instanceName.AsSpan(1), CultureInfo.InvariantCulture, out int id) ? id : null;
        }

        private static string NormalizeArchivePath(string path)
        {
            return path.Replace('\\', '/');
        }

        private sealed record MediaItem(string Name, string EntryName, float Width, float Height, string? BitmapDataEntryName);
    }
}
