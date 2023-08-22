using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace Dusty_Easel
{
    public class Drawing
    {
        private GetPixel getPixel;
        private Bitmap canvas;
        private Graphics g;
        private bool paint;
        private Point brush, brush2;
        private float scaleFactor = 10;
        public Color brushColor = Color.Black;
        private DrawingTool usingBrush = DrawingTool.Pencil;
        private PictureBox _easel;

        private enum DrawingTool
        {
            Pencil,
            Eraser
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

        private void Draw(Point point)
        {
            ActionPixel(point);
        }

        private void ActionPixel(Point pixel)
        {
            Color color = (usingBrush == DrawingTool.Pencil) ? brushColor : Color.Transparent;
            try
            {
                canvas.SetPixel(pixel.X, pixel.Y, color);
                ApplyZoom();
                _easel.Invalidate();
            }
            catch { }
        }

        private void ApplyZoom()
        {
            int newWidth = (int)(canvas.Width * scaleFactor);
            int newHeight = (int)(canvas.Height * scaleFactor);
            Bitmap scaledBitmap = new Bitmap(newWidth, newHeight);
            Graphics scaledGraphics = Graphics.FromImage(scaledBitmap);

            try
            {
                scaledGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                scaledGraphics.DrawImage(canvas, new Rectangle(0, 0, newWidth, newHeight));
                _easel.Image = scaledBitmap;
            }
            finally
            {
                scaledGraphics.Dispose();
            }
        }

        public void easel_MouseDown(MouseEventArgs e)
        {
            paint = true;
            brush = ConvertToScaledPoint(e.Location);
            brush2 = brush;
            Draw(brush);
        }

        public void easel_MouseUp(MouseEventArgs e)
        {
            paint = false;
        }

        public void easel_MouseMove(MouseEventArgs e)
        {
            if (!paint) return;

            brush = ConvertToScaledPoint(e.Location);

            if (usingBrush == DrawingTool.Pencil)
            {
                Draw(brush);
            }
            else if (usingBrush == DrawingTool.Eraser)
            {
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

        public void pencilButton_MouseDown()
        {
            usingBrush = DrawingTool.Pencil;
        }

        public void eraserButton_MouseDown()
        {
            usingBrush = DrawingTool.Eraser;
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
            canvas = new Bitmap(filePath);
            ApplyZoom();
        }

        public void NewBitmap(int Height, int Width)
        {
            canvas = new Bitmap(Width, Height);
            ApplyZoom();
        }
    }
}
