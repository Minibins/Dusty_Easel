using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Dusty_Easel;

public class PixelRenderer : IDisposable
{
    private int _textureWidth;
    private int _textureHeight;
    
    public static byte[] PixelData { get; private set; }
    
    public int TextureId { get; private set; }

    public Vector2i TextureSize => new(_textureWidth, _textureHeight);

    public PixelRenderer(int width = 64, int height = 64)
    {
        _textureWidth = width;
        _textureHeight = height;
        
        PixelData = new byte[_textureWidth * _textureHeight * 4];
        
        CreateFramebuffer();
        ClearToColor(new Vector4(1, 1, 1, 0)); 
    }

    private void CreateFramebuffer()
    {
        TextureId = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2d, TextureId);
        
        for (var i = 0; i < PixelData.Length; i += 4)
        {
            PixelData[i] = 255;     // R
            PixelData[i + 1] = 255; // G
            PixelData[i + 2] = 255; // B
            PixelData[i + 3] = 255; // A
        }
        
        GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, 
            _textureWidth, _textureHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, PixelData);
        
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
        
        PixelData[index] = (byte)(color.X * 255);
        PixelData[index + 1] = (byte)(color.Y * 255);
        PixelData[index + 2] = (byte)(color.Z * 255);
        PixelData[index + 3] = (byte)(color.W * 255);

        UpdateTexture();
    }

    public Vector4 GetPixel(int x, int y)
    {
        if (x < 0 || x >= _textureWidth || y < 0 || y >= _textureHeight)
            return Vector4.Zero;

        var index = (y * _textureWidth + x) * 4;
        
        return new Vector4(
            PixelData[index] / 255f,
            PixelData[index + 1] / 255f,
            PixelData[index + 2] / 255f,
            PixelData[index + 3] / 255f
        );
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

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    public void DrawCircle(Vector2i center, int radius, Vector4 color, bool filled = true)
    {
        if (filled)
        {
            for (var y = -radius; y <= radius; y++)
            {
                for (var x = -radius; x <= radius; x++)
                {
                    if (x * x + y * y <= radius * radius)
                    {
                        SetPixel(center.X + x, center.Y + y, color);
                    }
                }
            }
        }
        else
        {
            var x = radius;
            var y = 0;
            var err = 0;

            while (x >= y)
            {
                SetPixel(center.X + x, center.Y + y, color);
                SetPixel(center.X + y, center.Y + x, color);
                SetPixel(center.X - y, center.Y + x, color);
                SetPixel(center.X - x, center.Y + y, color);
                SetPixel(center.X - x, center.Y - y, color);
                SetPixel(center.X - y, center.Y - x, color);
                SetPixel(center.X + y, center.Y - x, color);
                SetPixel(center.X + x, center.Y - y, color);

                if (err <= 0)
                {
                    y += 1;
                    err += 2 * y + 1;
                }

                if (err <= 0) continue;
                x -= 1;
                err -= 2 * x + 1;
            }
        }
    }

    
    public void ClearToColor(Vector4 color)
    {
        for (var i = 0; i < PixelData.Length; i += 4)
        {
            PixelData[i] = (byte)(color.X * 255);
            PixelData[i + 1] = (byte)(color.Y * 255);
            PixelData[i + 2] = (byte)(color.Z * 255);
            PixelData[i + 3] = (byte)(color.W * 255);
        }
        UpdateTexture();
    }

    public void FloodFill(int x, int y, Vector4 targetColor, Vector4 replacementColor)
    {
        var originalColor = GetPixel(x, y);
        
        if (ColorsEqual(originalColor, replacementColor))
            return;

        if (!ColorsEqual(originalColor, targetColor))
            return;

        var pixels = new Queue<Vector2i>();
        pixels.Enqueue(new Vector2i(x, y));

        while (pixels.Count > 0)
        {
            var p = pixels.Dequeue();
            
            if (p.X < 0 || p.X >= _textureWidth || p.Y < 0 || p.Y >= _textureHeight)
                continue;

            Vector4 currentColor = GetPixel(p.X, p.Y);
            
            if (!ColorsEqual(currentColor, originalColor))
                continue;

            SetPixel(p.X, p.Y, replacementColor);

            pixels.Enqueue(new Vector2i(p.X + 1, p.Y));
            pixels.Enqueue(new Vector2i(p.X - 1, p.Y));
            pixels.Enqueue(new Vector2i(p.X, p.Y + 1));
            pixels.Enqueue(new Vector2i(p.X, p.Y - 1));
        }
    }

    private static bool ColorsEqual(Vector4 a, Vector4 b)
    {
        const float epsilon = 0.01f;
        return Math.Abs(a.X - b.X) < epsilon &&
               Math.Abs(a.Y - b.Y) < epsilon &&
               Math.Abs(a.Z - b.Z) < epsilon &&
               Math.Abs(a.W - b.W) < epsilon;
    }

    private void UpdateTexture()
    {
        GL.BindTexture(TextureTarget.Texture2d, TextureId);
        GL.TexSubImage2D(TextureTarget.Texture2d, 0, 0, 0, 
            _textureWidth, _textureHeight, PixelFormat.Rgba, PixelType.UnsignedByte, PixelData);
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