using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Dusty_Easel;

public class SaveLoadImage
{
    public static void SaveRgbaToPng(byte[] rgba, int width, int height, string path)
    {
        if (rgba.Length < width * height * 4)
            throw new ArgumentException("rgba buffer is too small");

        using var img = Image.LoadPixelData<Rgba32>(rgba, width, height);

        img.Save(path, new PngEncoder());
    }

    public static bool LoadPngToBytes(string path, out byte[]? rgba, out int width, out int height)
    {
        rgba = null;
        width = 0;
        height = 0;

        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));

        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found: {path}");

        var extension = Path.GetExtension(path).ToLowerInvariant();
        if (extension != ".png")
            Console.WriteLine($"Warning: File extension is '{extension}', expected '.png'");

        using var img = Image.Load<Rgba32>(path);

        width = img.Width;
        height = img.Height;

        rgba = new byte[width * height * 4];
        img.CopyPixelDataTo(rgba);

        return true;
    }
}