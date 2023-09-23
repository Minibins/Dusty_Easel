using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dusty_Easel
{
    public static class DocumentManager
    {
        public static Form1 form;
        public static void loadImage(PictureBox image)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image loadedImage = Image.FromFile(openFileDialog.FileName);
                        image.SizeMode = PictureBoxSizeMode.Zoom;
                        image.Image = loadedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message);
                    }
                }
            }
        }
        private static void newTab()
        {
           
            form.tabs.TabPages.Add(tabPage1);
        }
    }
}
