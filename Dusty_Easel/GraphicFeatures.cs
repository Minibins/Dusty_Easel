using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
namespace Dusty_Easel
{
    class GraphicFeatures
    {
        public Bitmap ApplyZoom(Bitmap canvas, float scaleFactor, Bitmap _easel)//Перезагрузка с картинкой в качестве цели
        {
            int newWidth = (int)(canvas.Width * scaleFactor);
            int newHeight = (int)(canvas.Height * scaleFactor);
            Bitmap scaledBitmap = new Bitmap(Math.Min(newWidth, _easel.Width), Math.Min(newHeight, _easel.Height));
            Graphics scaledGraphics = Graphics.FromImage(scaledBitmap);
            Bitmap fixedCanvas = new Bitmap(canvas.Width + 1, canvas.Height + 1);
            Graphics extender = Graphics.FromImage(fixedCanvas);
            try
            {
                extender.DrawImage(canvas, new Rectangle(1, 1, canvas.Width, canvas.Height));
                scaledGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                scaledGraphics.DrawImage(fixedCanvas, new Rectangle((int)(scaleFactor / 2 - scaleFactor), (int)(scaleFactor / 2 - scaleFactor), newWidth, newHeight));
            }
            finally
            {
                scaledGraphics.Dispose();
                extender.Dispose();
            }
            return scaledBitmap;
        }
        public Bitmap ApplyZoom(Bitmap canvas, float scaleFactor, PictureBox _easel)//Перезагрузка с коробкой картинки в качестве цели
        {
            int newWidth = (int)(canvas.Width * scaleFactor);
            int newHeight = (int)(canvas.Height * scaleFactor);
            Bitmap scaledBitmap = new Bitmap(Math.Min(newWidth, _easel.Width), Math.Min(newHeight, _easel.Height));
            Graphics scaledGraphics = Graphics.FromImage(scaledBitmap);
            Bitmap fixedCanvas = new Bitmap(canvas.Width + 1, canvas.Height + 1);
            Graphics extender = Graphics.FromImage(fixedCanvas);
            try
            {
                extender.DrawImage(canvas, new Rectangle(1, 1, canvas.Width, canvas.Height));
                scaledGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                scaledGraphics.DrawImage(fixedCanvas, new Rectangle((int)(scaleFactor / 2 - scaleFactor), (int)(scaleFactor / 2 - scaleFactor), newWidth, newHeight));
            }
            finally
            {
                scaledGraphics.Dispose();
                extender.Dispose();
            }
            return scaledBitmap;
        }
        public Bitmap ApplyZoom(Bitmap canvas, PictureBox _easel)
        {
            int scaleFactor = 4;

            int newWidth = (int)(canvas.Width * scaleFactor);
            int newHeight = (int)(canvas.Height * scaleFactor);

            Bitmap scaledBitmap = new Bitmap(Math.Max(newWidth,1), Math.Max(newHeight, 1));

            using (Graphics scaledGraphics = Graphics.FromImage(scaledBitmap))
            {
                scaledGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                scaledGraphics.DrawImage(canvas, new Rectangle(0, 0, newWidth, newHeight));
            }
            
            return scaledBitmap;
            
        }
        public Bitmap ReplaceAreaWithImage(Bitmap mainImage, Bitmap insertedImage, Color Color)
        {

            List<Point> PixelStorePlace = new List<Point>();
            Point shapeleftup = new Point(0, 0);
            Point shaperightdown = new Point(0, 0);
            for (int x = 0; x < mainImage.Width; x++)
            {
                for (int y = 0; y < mainImage.Height; y++)
                {
                    Color pixelColor = mainImage.GetPixel(x, y);
                    if (pixelColor == Color)
                    {
                        PixelStorePlace.Add(new Point(x, y));
                        if (shapeleftup == null) shapeleftup = new Point(x, y);
                        else shapeleftup = new Point(Math.Min(shapeleftup.X, x), Math.Min(shapeleftup.Y, y));
                        if (shaperightdown == null) shaperightdown = new Point(x, y);
                        else shaperightdown = new Point(Math.Min(shaperightdown.X, x), Math.Min(shaperightdown.Y, y));
                    }
                }
            }
            Bitmap scaledImage = new Bitmap(insertedImage, new Size(shaperightdown.X - shapeleftup.X + 1, shaperightdown.Y - shapeleftup.Y + 1));
            Point[] PixelsToReplace = PixelStorePlace.ToArray();
            int a = PixelsToReplace.Length-1;
            while (a == 0)
            {
                mainImage.SetPixel(
                    shapeleftup.X + PixelsToReplace[a].X,
                    shapeleftup.Y + PixelsToReplace[a].Y,
                    scaledImage.GetPixel(PixelsToReplace[a].X, PixelsToReplace[a].Y)
                );
                a--;

            }
            return mainImage;
        }
    }
}
