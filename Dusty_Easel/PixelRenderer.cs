using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Dusty_Easel;

public class PixelRenderer : IDisposable
{
    private int _textureWidth;
    private int _textureHeight;

    public static byte[]? PixelData { get; set; }

    public int TextureId { get; private set; }

    public Vector2i TextureSize => new(_textureWidth, _textureHeight);

    private readonly Vector4 _transparentColor = new(0, 0, 0, 0);


    public PixelRenderer(int width = 64, int height = 64)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Width and height must be positive");
        
        _textureWidth = width;
        _textureHeight = height;

        PixelData = new byte[_textureWidth * _textureHeight * 4];

        CreateFramebuffer();
        ClearToColor(_transparentColor);
    }

    private void CreateFramebuffer()
    {
        TextureId = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2d, TextureId);

        for (var i = 0; i < PixelData!.Length; i += 4)
            WritePixelToData(_transparentColor, i);
        

        GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, _textureWidth, _textureHeight, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, PixelData);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        GL.BindTexture(TextureTarget.Texture2d, 0);
    }

    public void SetPixel(int x, int y, Vector4 color)
    {
        if (x < 0 || x >= _textureWidth || y < 0 || y >= _textureHeight)
            return;

        var index = (y * _textureWidth + x) * 4;

        WritePixelToData(color, index);

        UpdateTexture();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WritePixelToData(Vector4 color, int index)
    {
        PixelData![index] = (byte)Math.Clamp(color.X * 255, 0, 255);
        PixelData[index + 1] = (byte)Math.Clamp(color.Y * 255, 0, 255);
        PixelData[index + 2] = (byte)Math.Clamp(color.Z * 255, 0, 255);
        PixelData[index + 3] = (byte)Math.Clamp(color.W * 255, 0, 255);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector4 LoadPixelFromData(int index) =>
        new(PixelData![index] / 255f, PixelData[index + 1] / 255f, PixelData[index + 2] / 255f, PixelData[index + 3] / 255f);


    public Vector4 GetPixel(int x, int y)
    {
        if (x < 0 || x >= _textureWidth || y < 0 || y >= _textureHeight)
            return Vector4.Zero;
        
        return LoadPixelFromData((y * _textureWidth + x) * 4);
    }

    public void DrawLine(Vector2i start, Vector2i end, Vector4 color)
    {
        int x0 = start.X, y0 = start.Y;
        int x1 = end.X, y1 = end.Y;

        var dx = Math.Abs(x1 - x0);
        var dy = Math.Abs(y1 - y0);
        var sx = x0 < x1 ? 1 : -1;
        var sy = y0 < y1 ? 1 : -1;
        var err = dx - dy;

        while (true)
        {
            SetPixel(x0, y0, color);

            if (x0 == x1 && y0 == y1) break;

            var e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }

            if (e2 >= dx) continue;
            
            err += dx;
            y0 += sy;
        }
    }


    public void ClearToColor(Vector4 color)
    {
        for (var i = 0; i < PixelData!.Length; i += 4)
            WritePixelToData(color, i);

        UpdateTexture();
    }

    private void UpdateTexture()
    {
        GL.BindTexture(TextureTarget.Texture2d, TextureId);
        GL.TexSubImage2D(TextureTarget.Texture2d, 0, 0, 0, _textureWidth, _textureHeight, PixelFormat.Rgba, PixelType.UnsignedByte, PixelData);
        GL.BindTexture(TextureTarget.Texture2d, 0);
    }

    public void Resize(int newWidth, int newHeight)
    {
        _textureWidth = newWidth;
        _textureHeight = newHeight;

        PixelData = new byte[_textureWidth * _textureHeight * 4];

        GL.DeleteTexture(TextureId);
        CreateFramebuffer();
        ClearToColor(new Vector4(1, 1, 1, 1));
    }

    public void Dispose() => GL.DeleteTexture(TextureId);
}