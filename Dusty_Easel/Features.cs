using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
namespace Dusty_Easel
{
    static class Features
    {
       
        public static Bitmap ApplyZoom(Bitmap canvas, float scaleFactor, Bitmap _easel)//Перезагрузка с картинкой в качестве цели
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
        public static Bitmap ApplyZoom(Bitmap canvas, float scaleFactor, PictureBox _easel)//Перезагрузка с коробкой картинки в качестве цели
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
        public static Bitmap ApplyZoom(Bitmap canvas, PictureBox _easel)
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
        public static Bitmap ReplaceColorAreaWithImage(Bitmap sourceImage, Bitmap replacementImage, Color targetColor)
        {
            // Создаем копию исходного изображения, чтобы не изменять оригинал
            Bitmap resultImage = new Bitmap(sourceImage);

            // Находим границы области с заданным цветом
            int minX = sourceImage.Width;
            int minY = sourceImage.Height;
            int maxX = 0;
            int maxY = 0;

            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    if (isThatColor(targetColor,sourceImage,x,y))
                    {
                        // Обновляем границы области
                        minX = Math.Min(minX, x);
                        minY = Math.Min(minY, y);
                        maxX = Math.Max(maxX, x);
                        maxY = Math.Max(maxY, y);
                    }
                }
            }

            // Вычисляем размер области
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;
            Bitmap resizedReplacementImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedReplacementImage))
            {
                g.InterpolationMode=InterpolationMode.NearestNeighbor;
                g.DrawImage(replacementImage, new Rectangle(2, 2, width, height));
            }
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Color pixelColor = sourceImage.GetPixel(x, y);
                    if (pixelColor == targetColor)
                    {
                        resultImage.SetPixel(x, y, resizedReplacementImage.GetPixel(x - minX, y - minY));
                    }
                }
            }

            return resultImage;
        }
        public static void saveTXTatSystemFiles(string TXTname, int maxLines, string Text)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DustyEasel");

            // Полный путь к файлу, включая имя пользователя
            string filePath = Path.Combine(folderPath, TXTname);
      

            try
            {
                // Читаем существующий файл, если он существует
                string[] existingLines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];

                // Добавляем новую строку в начало
                string[] linesToWrite = new string[existingLines.Length + 1];
                linesToWrite[0] = Text;

                // Копируем остальные строки из существующего файла
                Array.Copy(existingLines, 0, linesToWrite, 1, Math.Min(existingLines.Length, maxLines - 1));

                // Если превышен лимит строк, убираем лишние
                if (linesToWrite.Length > maxLines)
                {
                    Array.Resize(ref linesToWrite, maxLines);
                }

                // Записываем обновленные строки в файл
                File.WriteAllLines(filePath+"txt", linesToWrite);

                
            }
            catch
            {
               
            }
        }
        public static Color GetPixelColor(PictureBox Image, MouseEventArgs e)
        {
            Bitmap image = (Bitmap)Image.Image;

            if (e.X >= 0 && e.X < image.Width && e.Y >= 0 && e.Y < image.Height)
            {
                Color pixelColor = image.GetPixel(e.X, e.Y);
                return pixelColor;
            }

            return Color.Empty;
        }
        public static bool isThatColor(Color c, Bitmap b, int x,int y) 
        {
            return b.GetPixel(x,y) == c;
        }
        
    }
}
