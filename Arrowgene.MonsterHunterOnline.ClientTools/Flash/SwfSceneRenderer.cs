using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using SkiaSharp;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Flash;

/// <summary>
/// Renders the first frame of an SWF file to a PNG bitmap using SkiaSharp.
/// Handles bitmap characters (DefineBitsLossless2, DefineBitsJPEG3),
/// vector shapes (DefineShape with solid/bitmap fills and strokes),
/// sprites (DefineSprite), and display list placement (PlaceObject2).
/// </summary>
public static class SwfSceneRenderer
{
    public static byte[]? RenderFirstFrame(SwfFile swf, SKColor? backgroundColor = null)
    {
        int width = (int)MathF.Ceiling(swf.FrameSize.WidthPixels);
        int height = (int)MathF.Ceiling(swf.FrameSize.HeightPixels);
        if (width <= 0 || height <= 0 || width > 4096 || height > 4096)
        {
            return null;
        }

        try
        {
            Scene scene = ParseScene(swf);
            using SKSurface surface = SKSurface.Create(new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul));
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(backgroundColor ?? new SKColor(204, 204, 204, 255));

            RenderDisplayList(canvas, scene, scene.RootDisplayList, SKMatrix.Identity, width, height);

            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 90);
            return data.ToArray();
        }
        catch
        {
            return null;
        }
    }

    private static void RenderDisplayList(SKCanvas canvas, Scene scene, List<PlaceEntry> displayList,
        SKMatrix parentMatrix, int stageW, int stageH)
    {
        foreach (PlaceEntry entry in displayList)
        {
            SKMatrix localMatrix = Multiply(parentMatrix, entry.Matrix);

            if (scene.Sprites.TryGetValue(entry.CharacterId, out SpriteChar? sprite))
            {
                RenderDisplayList(canvas, scene, sprite.DisplayList, localMatrix, stageW, stageH);
                continue;
            }

            if (scene.Shapes.TryGetValue(entry.CharacterId, out ShapeChar? shape))
            {
                RenderShape(canvas, scene, shape, localMatrix);
            }
        }
    }

    private static void RenderShape(SKCanvas canvas, Scene scene, ShapeChar shape, SKMatrix matrix)
    {
        canvas.Save();
        canvas.SetMatrix(Multiply(canvas.TotalMatrix, matrix));

        // Draw fills
        foreach (ShapePath shapePath in shape.Paths)
        {
            if (shapePath.FillStyle == null || shapePath.Points.Count < 2)
            {
                continue;
            }

            using SKPath path = BuildSkPath(shapePath.Points);
            FillStyleDef fill = shapePath.FillStyle;

            if (fill.Type == FillType.Solid)
            {
                using SKPaint paint = new() { Color = fill.Color, IsAntialias = true, Style = SKPaintStyle.Fill };
                canvas.DrawPath(path, paint);
            }
            else if (fill.Type == FillType.Bitmap && fill.BitmapId.HasValue)
            {
                if (scene.Bitmaps.TryGetValue(fill.BitmapId.Value, out SKBitmap? bitmap))
                {
                    canvas.Save();
                    canvas.ClipPath(path, SKClipOperation.Intersect, true);

                    SKMatrix bitmapMatrix = fill.BitmapMatrix;
                    canvas.SetMatrix(Multiply(canvas.TotalMatrix, bitmapMatrix));
                    canvas.DrawBitmap(bitmap, 0, 0);
                    canvas.Restore();
                }
            }
        }

        // Draw strokes
        foreach (ShapePath shapePath in shape.Paths)
        {
            if (shapePath.LineStyle == null || shapePath.Points.Count < 2)
            {
                continue;
            }

            using SKPath path = BuildSkPath(shapePath.Points);
            LineStyleDef line = shapePath.LineStyle;

            using SKPaint paint = new()
            {
                Color = line.Color,
                StrokeWidth = line.Width,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
            };
            canvas.DrawPath(path, paint);
        }

        canvas.Restore();
    }

    private static SKPath BuildSkPath(List<PathPoint> points)
    {
        SKPath path = new();
        foreach (PathPoint pt in points)
        {
            switch (pt.Type)
            {
                case PathPointType.MoveTo:
                    path.MoveTo(pt.X, pt.Y);
                    break;
                case PathPointType.LineTo:
                    path.LineTo(pt.X, pt.Y);
                    break;
                case PathPointType.CurveTo:
                    path.QuadTo(pt.Cx, pt.Cy, pt.X, pt.Y);
                    break;
            }
        }

        path.Close();
        return path;
    }

    // ── Scene parsing ──

    private static Scene ParseScene(SwfFile swf)
    {
        byte[] raw = swf.GetUncompressedBytes();
        Scene scene = new();

        // First pass: parse all character definitions
        foreach (SwfTag tag in swf.Tags)
        {
            switch (tag.Code)
            {
                case 20: // DefineBitsLossless
                case 36: // DefineBitsLossless2
                    ParseDefineBitsLossless(scene, tag);
                    break;
                case 21: // DefineBitsJPEG2
                case 35: // DefineBitsJPEG3
                case 90: // DefineBitsJPEG4
                    ParseDefineBitsJpeg(scene, tag);
                    break;
                case 2:  // DefineShape
                case 22: // DefineShape2
                case 32: // DefineShape3
                case 83: // DefineShape4
                    ParseDefineShape(scene, tag);
                    break;
                case 39: // DefineSprite
                    ParseDefineSprite(scene, tag, raw);
                    break;
            }
        }

        // Second pass: parse root display list (PlaceObject2 tags)
        foreach (SwfTag tag in swf.Tags)
        {
            if (tag.Code is 26 or 70) // PlaceObject2/3
            {
                PlaceEntry? entry = ParsePlaceObject2(tag.Data.Span);
                if (entry != null)
                {
                    scene.RootDisplayList.Add(entry);
                }
            }

            if (tag.Code == 1) // ShowFrame - stop after frame 1
            {
                break;
            }
        }

        return scene;
    }

    // ── Bitmap parsing ──

    private static void ParseDefineBitsLossless(Scene scene, SwfTag tag)
    {
        ReadOnlySpan<byte> data = tag.Data.Span;
        if (data.Length < 7) return;

        ushort characterId = BinaryPrimitives.ReadUInt16LittleEndian(data);
        byte format = data[2];
        ushort width = BinaryPrimitives.ReadUInt16LittleEndian(data[3..]);
        ushort height = BinaryPrimitives.ReadUInt16LittleEndian(data[5..]);
        bool hasAlpha = tag.Code == 36;

        int headerSize = 7;
        if (format == 3) headerSize = 8; // palette count byte

        byte[] decompressed;
        try
        {
            decompressed = Inflate(data[headerSize..]);
        }
        catch
        {
            return;
        }

        SKBitmap? bitmap = null;
        if (format == 5) // 32-bit ARGB
        {
            bitmap = DecodeLosslessArgb(width, height, decompressed, hasAlpha);
        }
        else if (format == 3) // 8-bit colormapped
        {
            int paletteCount = data[7] + 1;
            bitmap = DecodeLosslessPaletted(width, height, decompressed, paletteCount, hasAlpha);
        }

        if (bitmap != null)
        {
            scene.Bitmaps[characterId] = bitmap;
        }
    }

    private static SKBitmap DecodeLosslessArgb(int width, int height, byte[] data, bool hasAlpha)
    {
        // SWF DefineBitsLossless2 format 5: each pixel is 4 bytes, premultiplied ARGB.
        // Rows are padded to 4-byte boundaries (for format 5, stride = width * 4, already aligned).
        int stride = width * 4;
        SKBitmap bitmap = new(width, height, SKColorType.Rgba8888, SKAlphaType.Unpremul);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int srcOff = y * stride + x * 4;
                if (srcOff + 3 >= data.Length) break;

                byte a = hasAlpha ? data[srcOff] : (byte)255;
                byte r = data[srcOff + 1];
                byte g = data[srcOff + 2];
                byte b = data[srcOff + 3];

                // Un-premultiply
                if (a > 0 && a < 255)
                {
                    r = (byte)Math.Min(255, r * 255 / a);
                    g = (byte)Math.Min(255, g * 255 / a);
                    b = (byte)Math.Min(255, b * 255 / a);
                }

                bitmap.SetPixel(x, y, new SKColor(r, g, b, a));
            }
        }

        return bitmap;
    }

    private static SKBitmap DecodeLosslessPaletted(int width, int height, byte[] data, int paletteCount, bool hasAlpha)
    {
        int bytesPerColor = hasAlpha ? 4 : 3;
        int paletteBytes = paletteCount * bytesPerColor;
        int paddedWidth = (width + 3) & ~3; // rows padded to 4-byte boundaries

        SKBitmap bitmap = new(width, height, SKColorType.Rgba8888, SKAlphaType.Unpremul);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int idx = paletteBytes + y * paddedWidth + x;
                if (idx >= data.Length) break;

                int colorIdx = data[idx];
                if (colorIdx >= paletteCount) colorIdx = 0;

                int palOff = colorIdx * bytesPerColor;
                byte r = data[palOff];
                byte g = data[palOff + 1];
                byte b = data[palOff + 2];
                byte a = hasAlpha && palOff + 3 < paletteBytes ? data[palOff + 3] : (byte)255;

                bitmap.SetPixel(x, y, new SKColor(r, g, b, a));
            }
        }

        return bitmap;
    }

    private static void ParseDefineBitsJpeg(Scene scene, SwfTag tag)
    {
        ReadOnlySpan<byte> data = tag.Data.Span;
        if (data.Length < 4) return;

        ushort characterId = BinaryPrimitives.ReadUInt16LittleEndian(data);
        int offset = 2;
        bool hasAlpha = tag.Code is 35 or 90;

        uint alphaOffset = 0;
        if (hasAlpha)
        {
            if (data.Length < 6) return;
            alphaOffset = BinaryPrimitives.ReadUInt32LittleEndian(data[offset..]);
            offset += 4;
        }

        if (tag.Code == 90) offset += 2; // deblock parameter

        ReadOnlySpan<byte> imageData;
        ReadOnlySpan<byte> alphaData = default;
        if (hasAlpha && alphaOffset > 0)
        {
            imageData = data.Slice(offset, (int)alphaOffset);
            alphaData = data[(offset + (int)alphaOffset)..];
        }
        else
        {
            imageData = data[offset..];
        }

        SKBitmap? bitmap = SKBitmap.Decode(imageData.ToArray());
        if (bitmap == null) return;

        // Apply alpha channel if present
        if (!alphaData.IsEmpty)
        {
            try
            {
                byte[] alpha = Inflate(alphaData);
                bitmap = ApplyAlphaChannel(bitmap, alpha);
            }
            catch
            {
            }
        }

        scene.Bitmaps[characterId] = bitmap;
    }

    private static SKBitmap ApplyAlphaChannel(SKBitmap source, byte[] alpha)
    {
        SKBitmap result = new(source.Width, source.Height, SKColorType.Rgba8888, SKAlphaType.Unpremul);
        for (int y = 0; y < source.Height; y++)
        {
            for (int x = 0; x < source.Width; x++)
            {
                int alphaIdx = y * source.Width + x;
                byte a = alphaIdx < alpha.Length ? alpha[alphaIdx] : (byte)255;
                SKColor c = source.GetPixel(x, y);
                result.SetPixel(x, y, new SKColor(c.Red, c.Green, c.Blue, a));
            }
        }

        source.Dispose();
        return result;
    }

    // ── Shape parsing ──

    private static void ParseDefineShape(Scene scene, SwfTag tag)
    {
        ReadOnlySpan<byte> data = tag.Data.Span;
        if (data.Length < 4) return;

        ushort characterId = BinaryPrimitives.ReadUInt16LittleEndian(data);
        BitReader bits = new(data[2..]);

        // Shape bounds RECT
        ReadRect(ref bits);

        // DefineShape4 has additional edge bounds and flags
        if (tag.Code == 83)
        {
            ReadRect(ref bits); // edge bounds
            bits.AlignByte();
            bits.ReadUBits(5); // reserved
            bits.ReadUBits(1); // UsesFillWindingRule
            bits.ReadUBits(1); // UsesNonScalingStrokes
            bits.ReadUBits(1); // UsesScalingStrokes
        }

        bits.AlignByte();

        int shapeVersion = tag.Code switch
        {
            2 => 1,
            22 => 2,
            32 => 3,
            83 => 4,
            _ => 1
        };

        // Parse fill styles and line styles
        List<FillStyleDef> fillStyles = ReadFillStyleArray(ref bits, shapeVersion);
        List<LineStyleDef> lineStyles = ReadLineStyleArray(ref bits, shapeVersion);

        // Parse shape records
        ShapeChar shape = new() { CharacterId = characterId };
        ReadShapeRecords(ref bits, fillStyles, lineStyles, shape, shapeVersion);

        scene.Shapes[characterId] = shape;
    }

    private static List<FillStyleDef> ReadFillStyleArray(ref BitReader bits, int shapeVersion)
    {
        List<FillStyleDef> styles = [];
        int count = bits.ReadByte();
        if (count == 0xFF && shapeVersion >= 2)
        {
            count = bits.ReadUI16();
        }

        for (int i = 0; i < count; i++)
        {
            styles.Add(ReadFillStyle(ref bits, shapeVersion));
        }

        return styles;
    }

    private static FillStyleDef ReadFillStyle(ref BitReader bits, int shapeVersion)
    {
        byte type = bits.ReadByte();

        if (type == 0x00) // Solid fill
        {
            SKColor color = shapeVersion >= 3 ? ReadRgba(ref bits) : ReadRgb(ref bits);
            return new FillStyleDef { Type = FillType.Solid, Color = color };
        }

        if (type is 0x10 or 0x12 or 0x13) // Gradient fill
        {
            ReadMatrix(ref bits); // gradient matrix
            bits.AlignByte();

            // Skip gradient records
            int spreadMode = (int)bits.ReadUBits(2);
            int interpMode = (int)bits.ReadUBits(2);
            int numGradients = (int)bits.ReadUBits(4);
            for (int i = 0; i < numGradients; i++)
            {
                bits.ReadByte(); // ratio
                if (shapeVersion >= 3) ReadRgba(ref bits); else ReadRgb(ref bits);
            }

            if (type == 0x13)
            {
                bits.ReadUI16(); // focal point
            }

            // Return a placeholder gradient fill (render as mid-gray)
            return new FillStyleDef { Type = FillType.Solid, Color = new SKColor(160, 160, 160, 255) };
        }

        if (type is 0x40 or 0x41 or 0x42 or 0x43) // Bitmap fill
        {
            ushort bitmapId = bits.ReadUI16();
            SKMatrix bitmapMatrix = ReadMatrix(ref bits);
            bits.AlignByte();

            // The bitmap fill matrix maps bitmap pixels to shape twip-space.
            // Shape coordinates are already converted to pixels (/20), so
            // scale/rotate must also be divided by 20 (translate already is).
            bitmapMatrix = new SKMatrix(
                bitmapMatrix.ScaleX / 20f, bitmapMatrix.SkewX / 20f, bitmapMatrix.TransX,
                bitmapMatrix.SkewY / 20f, bitmapMatrix.ScaleY / 20f, bitmapMatrix.TransY,
                0, 0, 1);

            return new FillStyleDef { Type = FillType.Bitmap, BitmapId = bitmapId, BitmapMatrix = bitmapMatrix };
        }

        return new FillStyleDef { Type = FillType.Solid, Color = SKColors.Magenta };
    }

    private static List<LineStyleDef> ReadLineStyleArray(ref BitReader bits, int shapeVersion)
    {
        List<LineStyleDef> styles = [];
        int count = bits.ReadByte();
        if (count == 0xFF && shapeVersion >= 2)
        {
            count = bits.ReadUI16();
        }

        for (int i = 0; i < count; i++)
        {
            if (shapeVersion >= 4)
            {
                styles.Add(ReadLineStyle2(ref bits));
            }
            else
            {
                float width = bits.ReadUI16() / 20f;
                SKColor color = shapeVersion >= 3 ? ReadRgba(ref bits) : ReadRgb(ref bits);
                styles.Add(new LineStyleDef { Width = width, Color = color });
            }
        }

        return styles;
    }

    private static LineStyleDef ReadLineStyle2(ref BitReader bits)
    {
        float width = bits.ReadUI16() / 20f;
        bits.ReadUBits(2); // StartCapStyle
        int joinStyle = (int)bits.ReadUBits(2);
        bool hasFill = bits.ReadUBits(1) == 1;
        bits.ReadUBits(1); // NoHScaleFlag
        bits.ReadUBits(1); // NoVScaleFlag
        bits.ReadUBits(1); // PixelHintingFlag
        bits.ReadUBits(5); // Reserved
        bits.ReadUBits(1); // NoClose
        bits.ReadUBits(2); // EndCapStyle

        if (joinStyle == 2) bits.ReadUI16(); // MiterLimitFactor

        SKColor color;
        if (hasFill)
        {
            FillStyleDef fill = ReadFillStyle(ref bits, 4);
            color = fill.Color;
        }
        else
        {
            color = ReadRgba(ref bits);
        }

        return new LineStyleDef { Width = width, Color = color };
    }

    private static void ReadShapeRecords(ref BitReader bits, List<FillStyleDef> fillStyles,
        List<LineStyleDef> lineStyles, ShapeChar shape, int shapeVersion)
    {
        bits.AlignByte();
        int numFillBits = (int)bits.ReadUBits(4);
        int numLineBits = (int)bits.ReadUBits(4);

        float curX = 0, curY = 0;
        int fillStyle0Idx = 0, fillStyle1Idx = 0, lineStyleIdx = 0;

        List<PathPoint> currentPoints = [new PathPoint { Type = PathPointType.MoveTo, X = 0, Y = 0 }];

        while (true)
        {
            uint typeFlag = bits.ReadUBits(1);

            if (typeFlag == 0) // Non-edge record
            {
                uint stateNewStyles = bits.ReadUBits(1);
                uint stateLineStyle = bits.ReadUBits(1);
                uint stateFillStyle1 = bits.ReadUBits(1);
                uint stateFillStyle0 = bits.ReadUBits(1);
                uint stateMoveTo = bits.ReadUBits(1);

                if (stateNewStyles == 0 && stateLineStyle == 0 && stateFillStyle1 == 0 &&
                    stateFillStyle0 == 0 && stateMoveTo == 0)
                {
                    // End of shape
                    FlushPath(shape, currentPoints, fillStyles, lineStyles, fillStyle0Idx, fillStyle1Idx, lineStyleIdx);
                    break;
                }

                // Flush current sub-path before style change
                FlushPath(shape, currentPoints, fillStyles, lineStyles, fillStyle0Idx, fillStyle1Idx, lineStyleIdx);

                if (stateMoveTo == 1)
                {
                    int moveBits = (int)bits.ReadUBits(5);
                    curX = bits.ReadSBits(moveBits) / 20f;
                    curY = bits.ReadSBits(moveBits) / 20f;
                }

                if (stateFillStyle0 == 1) fillStyle0Idx = (int)bits.ReadUBits(numFillBits);
                if (stateFillStyle1 == 1) fillStyle1Idx = (int)bits.ReadUBits(numFillBits);
                if (stateLineStyle == 1) lineStyleIdx = (int)bits.ReadUBits(numLineBits);

                if (stateNewStyles == 1 && shapeVersion >= 2)
                {
                    bits.AlignByte();
                    fillStyles = ReadFillStyleArray(ref bits, shapeVersion);
                    lineStyles = ReadLineStyleArray(ref bits, shapeVersion);
                    bits.AlignByte();
                    numFillBits = (int)bits.ReadUBits(4);
                    numLineBits = (int)bits.ReadUBits(4);
                }

                currentPoints = [new PathPoint { Type = PathPointType.MoveTo, X = curX, Y = curY }];
            }
            else // Edge record
            {
                uint straightFlag = bits.ReadUBits(1);
                int numBits = (int)bits.ReadUBits(4) + 2;

                if (straightFlag == 1) // Straight edge
                {
                    uint generalLine = bits.ReadUBits(1);
                    float dx = 0, dy = 0;
                    if (generalLine == 1)
                    {
                        dx = bits.ReadSBits(numBits) / 20f;
                        dy = bits.ReadSBits(numBits) / 20f;
                    }
                    else
                    {
                        uint vertLine = bits.ReadUBits(1);
                        if (vertLine == 0)
                            dx = bits.ReadSBits(numBits) / 20f;
                        else
                            dy = bits.ReadSBits(numBits) / 20f;
                    }

                    curX += dx;
                    curY += dy;
                    currentPoints.Add(new PathPoint { Type = PathPointType.LineTo, X = curX, Y = curY });
                }
                else // Curved edge
                {
                    float cx = bits.ReadSBits(numBits) / 20f;
                    float cy = bits.ReadSBits(numBits) / 20f;
                    float ax = bits.ReadSBits(numBits) / 20f;
                    float ay = bits.ReadSBits(numBits) / 20f;

                    float controlX = curX + cx;
                    float controlY = curY + cy;
                    float anchorX = controlX + ax;
                    float anchorY = controlY + ay;

                    currentPoints.Add(new PathPoint
                    {
                        Type = PathPointType.CurveTo,
                        Cx = controlX, Cy = controlY,
                        X = anchorX, Y = anchorY
                    });

                    curX = anchorX;
                    curY = anchorY;
                }
            }
        }
    }

    private static void FlushPath(ShapeChar shape, List<PathPoint> points,
        List<FillStyleDef> fillStyles, List<LineStyleDef> lineStyles,
        int fillStyle0Idx, int fillStyle1Idx, int lineStyleIdx)
    {
        if (points.Count < 2) return;

        FillStyleDef? fill = null;
        if (fillStyle1Idx > 0 && fillStyle1Idx <= fillStyles.Count)
            fill = fillStyles[fillStyle1Idx - 1];
        else if (fillStyle0Idx > 0 && fillStyle0Idx <= fillStyles.Count)
            fill = fillStyles[fillStyle0Idx - 1];

        LineStyleDef? line = lineStyleIdx > 0 && lineStyleIdx <= lineStyles.Count
            ? lineStyles[lineStyleIdx - 1]
            : null;

        if (fill != null || line != null)
        {
            shape.Paths.Add(new ShapePath
            {
                Points = new List<PathPoint>(points),
                FillStyle = fill,
                LineStyle = line,
            });
        }
    }

    // ── Sprite parsing ──

    private static void ParseDefineSprite(Scene scene, SwfTag tag, byte[] raw)
    {
        ReadOnlySpan<byte> data = tag.Data.Span;
        if (data.Length < 4) return;

        ushort characterId = BinaryPrimitives.ReadUInt16LittleEndian(data);
        // ushort frameCount = BinaryPrimitives.ReadUInt16LittleEndian(data[2..]);

        SpriteChar sprite = new() { CharacterId = characterId };

        // Parse nested tags within the sprite
        int offset = 4;
        while (offset + 2 <= data.Length)
        {
            ushort tagCodeAndLen = BinaryPrimitives.ReadUInt16LittleEndian(data[offset..]);
            ushort code = (ushort)(tagCodeAndLen >> 6);
            int length = tagCodeAndLen & 0x3F;
            offset += 2;

            if (length == 0x3F)
            {
                if (offset + 4 > data.Length) break;
                length = (int)BinaryPrimitives.ReadUInt32LittleEndian(data[offset..]);
                offset += 4;
            }

            if (offset + length > data.Length) break;

            if (code is 26 or 70) // PlaceObject2/3
            {
                PlaceEntry? entry = ParsePlaceObject2(data.Slice(offset, length));
                if (entry != null)
                {
                    sprite.DisplayList.Add(entry);
                }
            }

            if (code == 0) break; // End tag
            if (code == 1) break; // ShowFrame - stop after frame 1

            offset += length;
        }

        scene.Sprites[characterId] = sprite;
    }

    // ── PlaceObject2 parsing ──

    private static PlaceEntry? ParsePlaceObject2(ReadOnlySpan<byte> data)
    {
        if (data.Length < 3) return null;

        byte flags = data[0];
        bool hasCharacter = (flags & 0x02) != 0;
        bool hasMatrix = (flags & 0x04) != 0;

        // Flags byte layout (MSB to LSB in SWF bit convention, but stored as regular byte):
        // bit 7: HasClipActions, bit 6: HasClipDepth, bit 5: HasName, bit 4: HasRatio
        // bit 3: HasColorTransform, bit 2: HasMatrix, bit 1: HasCharacter, bit 0: Move

        int offset = 1;
        // ushort depth = BinaryPrimitives.ReadUInt16LittleEndian(data[offset..]);
        offset += 2;

        ushort characterId = 0;
        if (hasCharacter)
        {
            if (offset + 2 > data.Length) return null;
            characterId = BinaryPrimitives.ReadUInt16LittleEndian(data[offset..]);
            offset += 2;
        }
        else
        {
            return null; // Can't place without a character reference
        }

        SKMatrix matrix = SKMatrix.Identity;
        if (hasMatrix)
        {
            BitReader bits = new(data[offset..]);
            matrix = ReadMatrix(ref bits);
        }

        return new PlaceEntry { CharacterId = characterId, Matrix = matrix };
    }

    // ── SWF primitive readers ──

    private static SKMatrix ReadMatrix(ref BitReader bits)
    {
        float scaleX = 1f, scaleY = 1f;
        float rotateSkew0 = 0f, rotateSkew1 = 0f;
        float translateX = 0f, translateY = 0f;

        // HasScale
        if (bits.ReadUBits(1) == 1)
        {
            int nScaleBits = (int)bits.ReadUBits(5);
            scaleX = bits.ReadFixed(nScaleBits);
            scaleY = bits.ReadFixed(nScaleBits);
        }

        // HasRotate
        if (bits.ReadUBits(1) == 1)
        {
            int nRotateBits = (int)bits.ReadUBits(5);
            rotateSkew0 = bits.ReadFixed(nRotateBits);
            rotateSkew1 = bits.ReadFixed(nRotateBits);
        }

        // Translate (always present)
        int nTranslateBits = (int)bits.ReadUBits(5);
        if (nTranslateBits > 0)
        {
            translateX = bits.ReadSBits(nTranslateBits) / 20f;
            translateY = bits.ReadSBits(nTranslateBits) / 20f;
        }

        bits.AlignByte();

        // SWF matrix: [ScaleX, RotateSkew1, TranslateX]
        //             [RotateSkew0, ScaleY, TranslateY]
        return new SKMatrix(scaleX, rotateSkew1, translateX, rotateSkew0, scaleY, translateY, 0, 0, 1);
    }

    private static void ReadRect(ref BitReader bits)
    {
        int nbits = (int)bits.ReadUBits(5);
        bits.ReadSBits(nbits); // Xmin
        bits.ReadSBits(nbits); // Xmax
        bits.ReadSBits(nbits); // Ymin
        bits.ReadSBits(nbits); // Ymax
        bits.AlignByte();
    }

    private static SKColor ReadRgb(ref BitReader bits)
    {
        byte r = bits.ReadByte();
        byte g = bits.ReadByte();
        byte b = bits.ReadByte();
        return new SKColor(r, g, b, 255);
    }

    private static SKColor ReadRgba(ref BitReader bits)
    {
        byte r = bits.ReadByte();
        byte g = bits.ReadByte();
        byte b = bits.ReadByte();
        byte a = bits.ReadByte();
        return new SKColor(r, g, b, a);
    }

    private static SKMatrix Multiply(SKMatrix a, SKMatrix b)
    {
        return SKMatrix.Concat(a, b);
    }

    private static byte[] Inflate(ReadOnlySpan<byte> data)
    {
        using MemoryStream input = new(data.ToArray());
        // Skip zlib header if present
        if (data.Length >= 2 && data[0] == 0x78)
        {
            input.Position = 2;
        }

        using DeflateStream deflate = new(input, CompressionMode.Decompress);
        using MemoryStream output = new();
        deflate.CopyTo(output);
        return output.ToArray();
    }

    // ── Data model ──

    private sealed class Scene
    {
        public Dictionary<ushort, SKBitmap> Bitmaps { get; } = [];
        public Dictionary<ushort, ShapeChar> Shapes { get; } = [];
        public Dictionary<ushort, SpriteChar> Sprites { get; } = [];
        public List<PlaceEntry> RootDisplayList { get; } = [];
    }

    private sealed class PlaceEntry
    {
        public ushort CharacterId { get; init; }
        public SKMatrix Matrix { get; init; }
    }

    private sealed class SpriteChar
    {
        public ushort CharacterId { get; init; }
        public List<PlaceEntry> DisplayList { get; } = [];
    }

    private sealed class ShapeChar
    {
        public ushort CharacterId { get; init; }
        public List<ShapePath> Paths { get; } = [];
    }

    private sealed class ShapePath
    {
        public List<PathPoint> Points { get; init; } = [];
        public FillStyleDef? FillStyle { get; init; }
        public LineStyleDef? LineStyle { get; init; }
    }

    private enum PathPointType { MoveTo, LineTo, CurveTo }

    private struct PathPoint
    {
        public PathPointType Type;
        public float X, Y;
        public float Cx, Cy; // control point for curves
    }

    private enum FillType { Solid, Bitmap }

    private sealed class FillStyleDef
    {
        public FillType Type { get; init; }
        public SKColor Color { get; init; }
        public ushort? BitmapId { get; init; }
        public SKMatrix BitmapMatrix { get; init; }
    }

    private sealed class LineStyleDef
    {
        public float Width { get; init; }
        public SKColor Color { get; init; }
    }

    // ── Bit reader ──

    private ref struct BitReader
    {
        private readonly ReadOnlySpan<byte> _data;
        private int _bytePos;
        private int _bitPos; // 0-7, counts down from MSB (7) to LSB (0)

        public BitReader(ReadOnlySpan<byte> data)
        {
            _data = data;
            _bytePos = 0;
            _bitPos = 7;
        }

        public uint ReadUBits(int count)
        {
            uint result = 0;
            for (int i = 0; i < count; i++)
            {
                if (_bytePos >= _data.Length) return result;
                result = (result << 1) | (uint)((_data[_bytePos] >> _bitPos) & 1);
                _bitPos--;
                if (_bitPos < 0)
                {
                    _bitPos = 7;
                    _bytePos++;
                }
            }

            return result;
        }

        public int ReadSBits(int count)
        {
            uint raw = ReadUBits(count);
            if (count > 0 && (raw & (1u << (count - 1))) != 0)
            {
                // Sign extend
                raw |= uint.MaxValue << count;
            }

            return (int)raw;
        }

        public float ReadFixed(int count)
        {
            // SWF Fixed-point 16.16
            int raw = ReadSBits(count);
            return raw / 65536f;
        }

        public byte ReadByte()
        {
            AlignByte();
            if (_bytePos >= _data.Length) return 0;
            byte result = _data[_bytePos];
            _bytePos++;
            return result;
        }

        public ushort ReadUI16()
        {
            AlignByte();
            if (_bytePos + 2 > _data.Length) return 0;
            ushort result = BinaryPrimitives.ReadUInt16LittleEndian(_data[_bytePos..]);
            _bytePos += 2;
            return result;
        }

        public void AlignByte()
        {
            if (_bitPos != 7)
            {
                _bitPos = 7;
                _bytePos++;
            }
        }
    }
}
