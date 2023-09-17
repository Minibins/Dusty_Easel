using System.Drawing;
using System.Windows.Forms;

namespace Dusty_Easel
{
    public class GetPixel
    {
        public Color GetPixelColor(PictureBox Image, MouseEventArgs e)
        {
            Bitmap image = (Bitmap)Image.Image;

                if (e.X >= 0 && e.X < image.Width && e.Y >= 0 && e.Y < image.Height)
                {
                    Color pixelColor = image.GetPixel(e.X, e.Y);
                    return pixelColor;
                }

            return Color.Empty;
        }
    }
}
