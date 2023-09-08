using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace Dusty_Easel
{
    public class Drawing
    {
        private GetPixel getPixel;
        private Bitmap canvas;
        private Bitmap canvaswithprojection;
        private Graphics g;
        private bool paint;
        private Point brush, brush2;
        private float scaleFactor = 10;
        public Color brushColor = Color.Black;
        public DrawingTool usingBrush = DrawingTool.Pencil;
        private PictureBox _easel;
        private Bitmap scaledBitmap;
        public enum DrawingTool
        {
            Pencil,
            Eraser,
            Bucket
        }
        public void changeScale(float scale)
        {
            scaleFactor *= scale;
            scaleFactor = Math.Min(Math.Max(scaleFactor, 0.5f), 10);
            ApplyZoom();
        }
        public Drawing(PictureBox easel)
        {
            _easel = easel;
            InitializeCanvas();
            getPixel = new GetPixel();
        }

        private void InitializeCanvas()
        {
            canvas = new Bitmap(32,32);
            g = Graphics.FromImage(canvas);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            _easel.Image = canvas;
        }

        private void Draw(Point pixel, byte whichcanvas)
        {
            Color color = (usingBrush == DrawingTool.Pencil) ? brushColor : Color.Transparent;
            if (whichcanvas == 0) {try
            {
                canvas.SetPixel(pixel.X, pixel.Y, color);
                ApplyZoom();
                canvaswithprojection = canvas;
                _easel.Invalidate();
            }
            catch { } }
            else if (whichcanvas == 1)
            {
                canvaswithprojection = canvas;
                try
                {
                    canvaswithprojection.SetPixel(pixel.X, pixel.Y, color);
                    ApplyZoom();
                    _easel.Invalidate();
                }
                catch { }
            }
        }
        private void Fill(Point startPoint)
        {
            Color targetColor = canvas.GetPixel(startPoint.X, startPoint.Y);
            FloodFill(startPoint.X, startPoint.Y, targetColor, brushColor);
            ApplyZoom();
            _easel.Invalidate();
            return;

        }

        private void FloodFill(int x, int y, Color targetColor, Color replacementColor)
        {
            if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height)
                return;

            if (canvas.GetPixel(x, y) != targetColor)
                return;

            canvas.SetPixel(x, y, replacementColor);

            FloodFill(x - 1, y, targetColor, replacementColor);
            FloodFill(x + 1, y, targetColor, replacementColor);
            FloodFill(x, y - 1, targetColor, replacementColor);
            FloodFill(x, y + 1, targetColor, replacementColor);
            return;

        }

       
        private void ApplyZoom()
        {
            int newWidth = (int)(canvas.Width * scaleFactor);
            int newHeight = (int)(canvas.Height * scaleFactor);
            scaledBitmap = new Bitmap(Math.Min(newWidth, _easel.Width), Math.Min(newHeight, _easel.Width));
            Graphics scaledGraphics = Graphics.FromImage(scaledBitmap);

            try
            {
                scaledGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                scaledGraphics.DrawImage(canvaswithprojection, new Rectangle(0, 0, newWidth, newHeight));
                _easel.Image = scaledBitmap;
            }
            finally
            {
                scaledGraphics.Dispose();
                scaledBitmap.Dispose();
            }
            return;
        }

        public void easel_MouseDown(MouseEventArgs e)
        {
            paint = true;
            if (usingBrush == DrawingTool.Pencil)
            {
                brush = ConvertToScaledPoint(e.Location);
                brush2 = brush;
                Draw(brush,0);
            }
            else if (usingBrush == DrawingTool.Bucket)
            {
                Fill(ConvertToScaledPoint(e.Location));
            }
        }

        public void easel_MouseUp(MouseEventArgs e)
        {
            paint = false;
        }

        public void easel_MouseMove(MouseEventArgs e)
        {
            

            brush = ConvertToScaledPoint(e.Location);

            if (usingBrush == DrawingTool.Pencil)
            {
                if (paint) Draw(brush,0);
                if (paint) Draw(brush, 0);
            }
            else if (usingBrush == DrawingTool.Eraser)
            {
                if (!paint) return;
                ClearPixel(brush);
            }
           
        }
        
        private void ClearPixel(Point pixel)
        {
            try
            {
                canvas.SetPixel(pixel.X, pixel.Y, Color.Transparent);
                ApplyZoom();
                _easel.Invalidate();
            }
            catch { }
        }


        private Point ConvertToScaledPoint(Point point)
        {
            return new Point((int)(point.X / scaleFactor), (int)(point.Y / scaleFactor));
        }

      
        public void Palette_MouseClick(MouseEventArgs e, PictureBox Palette, PictureBox ColorNow)
        {
            brushColor = getPixel.GetPixelColor(Palette, e);
            ColorNow.BackColor = brushColor;
        }

        public void SaveAsPNG(string filePath)
        {
            canvas.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void OpenImage(string filePath)
        {
            canvaswithprojection = canvas = new Bitmap(filePath);
            ApplyZoom();
        }

        public void NewBitmap(int Height, int Width)
        {
            canvaswithprojection = canvas = new Bitmap(Width, Height);
            ApplyZoom();
        }
    }
}
