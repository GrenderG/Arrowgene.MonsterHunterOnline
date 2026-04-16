using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BitMiracle.LibTiff.Classic;
using Pfim;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal readonly record struct ImagePreviewDecodeResult(Bitmap Bitmap, PixelSize SourceSize);

internal static class ImagePreviewDecoder
{
    public static bool TryDecode(byte[] data,
        string? fileName,
        int maxWidth,
        out ImagePreviewDecodeResult result,
        out string? error)
    {
        string extension = Path.GetExtension(fileName ?? string.Empty);

        try
        {
            if (extension.Equals(".dds", StringComparison.OrdinalIgnoreCase))
            {
                return TryDecodeDds(data, maxWidth, out result, out error);
            }

            if (extension.Equals(".tif", StringComparison.OrdinalIgnoreCase) ||
                extension.Equals(".tiff", StringComparison.OrdinalIgnoreCase))
            {
                return TryDecodeTiff(data, maxWidth, out result, out error);
            }

            return TryDecodeWithAvalonia(data, maxWidth, out result, out error);
        }
        catch (Exception ex)
        {
            result = default;
            error = ex.Message;
            return false;
        }
    }

    private static bool TryDecodeWithAvalonia(byte[] data,
        int maxWidth,
        out ImagePreviewDecodeResult result,
        out string? error)
    {
        using MemoryStream stream = new MemoryStream(data, writable: false);
        Bitmap bitmap = new Bitmap(stream);
        PixelSize sourceSize = bitmap.PixelSize;
        Bitmap previewBitmap = ScaleBitmapIfNecessary(bitmap, maxWidth);
        if (!ReferenceEquals(previewBitmap, bitmap))
        {
            bitmap.Dispose();
        }

        result = new ImagePreviewDecodeResult(previewBitmap, sourceSize);
        error = null;
        return true;
    }

    private static bool TryDecodeDds(byte[] data,
        int maxWidth,
        out ImagePreviewDecodeResult result,
        out string? error)
    {
        using MemoryStream stream = new MemoryStream(data, writable: false);
        IImage image = Pfimage.FromStream(stream);

        try
        {
            if (image.Compressed)
            {
                image.Decompress();
            }

            PixelSize sourceSize = new PixelSize(image.Width, image.Height);
            Bitmap bitmap;
            switch (image.Format)
            {
                case ImageFormat.Rgba32:
                    bitmap = CreateBitmap(image.Data, image.Width, image.Height, PixelFormat.Bgra8888, AlphaFormat.Unpremul, image.Stride);
                    break;

                case ImageFormat.R5g6b5:
                    bitmap = CreateBitmap(ConvertRgb565ToBgra(image.Data, image.Width, image.Height, image.Stride),
                        image.Width,
                        image.Height,
                        PixelFormat.Bgra8888,
                        AlphaFormat.Opaque,
                        image.Width * 4);
                    break;

                case ImageFormat.R5g5b5:
                    bitmap = CreateBitmap(ConvertRgb555ToBgra(image.Data, image.Width, image.Height, image.Stride),
                        image.Width,
                        image.Height,
                        PixelFormat.Bgra8888,
                        AlphaFormat.Opaque,
                        image.Width * 4);
                    break;

                case ImageFormat.R5g5b5a1:
                    bitmap = CreateBitmap(ConvertRgb5A1ToBgra(image.Data, image.Width, image.Height, image.Stride),
                        image.Width,
                        image.Height,
                        PixelFormat.Bgra8888,
                        AlphaFormat.Unpremul,
                        image.Width * 4);
                    break;

                default:
                    result = default;
                    error = $"DDS pixel format {image.Format} is not supported yet.";
                    return false;
            }

            Bitmap previewBitmap = ScaleBitmapIfNecessary(bitmap, maxWidth);
            if (!ReferenceEquals(previewBitmap, bitmap))
            {
                bitmap.Dispose();
            }

            result = new ImagePreviewDecodeResult(previewBitmap, sourceSize);
            error = null;
            return true;
        }
        finally
        {
            (image as IDisposable)?.Dispose();
        }
    }

    private static bool TryDecodeTiff(byte[] data,
        int maxWidth,
        out ImagePreviewDecodeResult result,
        out string? error)
    {
        using MemoryStream stream = new MemoryStream(data, writable: false);
        using Tiff? tiff = Tiff.ClientOpen("memory", "r", stream, new ManagedTiffStream());
        if (tiff == null)
        {
            result = default;
            error = "Unable to open TIFF stream.";
            return false;
        }

        string rgbaError = string.Empty;
        if (!tiff.RGBAImageOK(out rgbaError))
        {
            result = default;
            error = string.IsNullOrWhiteSpace(rgbaError) ? "TIFF image cannot be converted to RGBA." : rgbaError;
            return false;
        }

        FieldValue[]? widthField = tiff.GetField(TiffTag.IMAGEWIDTH);
        FieldValue[]? heightField = tiff.GetField(TiffTag.IMAGELENGTH);
        if (widthField == null || heightField == null)
        {
            result = default;
            error = "TIFF image dimensions are unavailable.";
            return false;
        }

        int width = widthField[0].ToInt();
        int height = heightField[0].ToInt();
        int[] raster = new int[width * height];
        if (!tiff.ReadRGBAImageOriented(width, height, raster, Orientation.TOPLEFT))
        {
            result = default;
            error = "TIFF image could not be decoded.";
            return false;
        }

        byte[] bgra = ConvertTiffRasterToBgra(raster);
        Bitmap bitmap = CreateBitmap(bgra, width, height, PixelFormat.Bgra8888, AlphaFormat.Unpremul, width * 4);
        PixelSize sourceSize = new PixelSize(width, height);

        Bitmap previewBitmap = ScaleBitmapIfNecessary(bitmap, maxWidth);
        if (!ReferenceEquals(previewBitmap, bitmap))
        {
            bitmap.Dispose();
        }

        result = new ImagePreviewDecodeResult(previewBitmap, sourceSize);
        error = null;
        return true;
    }

    private static Bitmap CreateBitmap(byte[] data,
        int width,
        int height,
        PixelFormat pixelFormat,
        AlphaFormat alphaFormat,
        int stride)
    {
        GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try
        {
            return new WriteableBitmap(pixelFormat,
                alphaFormat,
                handle.AddrOfPinnedObject(),
                new PixelSize(width, height),
                new Vector(96, 96),
                stride);
        }
        finally
        {
            handle.Free();
        }
    }

    private static Bitmap ScaleBitmapIfNecessary(Bitmap bitmap, int maxWidth)
    {
        if (bitmap.PixelSize.Width <= maxWidth)
        {
            return bitmap;
        }

        int scaledHeight = Math.Max(1, (int)Math.Round(bitmap.PixelSize.Height * (maxWidth / (double)bitmap.PixelSize.Width)));
        return bitmap.CreateScaledBitmap(new PixelSize(maxWidth, scaledHeight), BitmapInterpolationMode.HighQuality);
    }

    private static byte[] ConvertTiffRasterToBgra(int[] raster)
    {
        byte[] result = new byte[raster.Length * 4];
        for (int i = 0; i < raster.Length; i++)
        {
            int pixel = raster[i];
            int offset = i * 4;
            result[offset] = (byte)Tiff.GetB(pixel);
            result[offset + 1] = (byte)Tiff.GetG(pixel);
            result[offset + 2] = (byte)Tiff.GetR(pixel);
            result[offset + 3] = (byte)Tiff.GetA(pixel);
        }

        return result;
    }

    private static byte[] ConvertRgb565ToBgra(byte[] data, int width, int height, int stride)
    {
        byte[] result = new byte[width * height * 4];
        for (int y = 0; y < height; y++)
        {
            int sourceRow = y * stride;
            int targetRow = y * width * 4;
            for (int x = 0; x < width; x++)
            {
                ushort pixel = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(sourceRow + x * 2, 2));
                byte red = Expand5To8((pixel >> 11) & 0x1F);
                byte green = Expand6To8((pixel >> 5) & 0x3F);
                byte blue = Expand5To8(pixel & 0x1F);
                int offset = targetRow + x * 4;
                result[offset] = blue;
                result[offset + 1] = green;
                result[offset + 2] = red;
                result[offset + 3] = 0xFF;
            }
        }

        return result;
    }

    private static byte[] ConvertRgb555ToBgra(byte[] data, int width, int height, int stride)
    {
        byte[] result = new byte[width * height * 4];
        for (int y = 0; y < height; y++)
        {
            int sourceRow = y * stride;
            int targetRow = y * width * 4;
            for (int x = 0; x < width; x++)
            {
                ushort pixel = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(sourceRow + x * 2, 2));
                byte red = Expand5To8((pixel >> 10) & 0x1F);
                byte green = Expand5To8((pixel >> 5) & 0x1F);
                byte blue = Expand5To8(pixel & 0x1F);
                int offset = targetRow + x * 4;
                result[offset] = blue;
                result[offset + 1] = green;
                result[offset + 2] = red;
                result[offset + 3] = 0xFF;
            }
        }

        return result;
    }

    private static byte[] ConvertRgb5A1ToBgra(byte[] data, int width, int height, int stride)
    {
        byte[] result = new byte[width * height * 4];
        for (int y = 0; y < height; y++)
        {
            int sourceRow = y * stride;
            int targetRow = y * width * 4;
            for (int x = 0; x < width; x++)
            {
                ushort pixel = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(sourceRow + x * 2, 2));
                byte alpha = (pixel & 0x8000) != 0 ? (byte)0xFF : (byte)0x00;
                byte red = Expand5To8((pixel >> 10) & 0x1F);
                byte green = Expand5To8((pixel >> 5) & 0x1F);
                byte blue = Expand5To8(pixel & 0x1F);
                int offset = targetRow + x * 4;
                result[offset] = blue;
                result[offset + 1] = green;
                result[offset + 2] = red;
                result[offset + 3] = alpha;
            }
        }

        return result;
    }

    private static byte Expand5To8(int value)
    {
        return (byte)((value << 3) | (value >> 2));
    }

    private static byte Expand6To8(int value)
    {
        return (byte)((value << 2) | (value >> 4));
    }

    private sealed class ManagedTiffStream : TiffStream
    {
        public override int Read(object clientData, byte[] buffer, int offset, int count)
        {
            return ((Stream)clientData).Read(buffer, offset, count);
        }

        public override void Write(object clientData, byte[] buffer, int offset, int count)
        {
            ((Stream)clientData).Write(buffer, offset, count);
        }

        public override long Seek(object clientData, long offset, SeekOrigin origin)
        {
            return ((Stream)clientData).Seek(offset, origin);
        }

        public override void Close(object clientData)
        {
            ((Stream)clientData).Dispose();
        }

        public override long Size(object clientData)
        {
            return ((Stream)clientData).Length;
        }
    }
}
