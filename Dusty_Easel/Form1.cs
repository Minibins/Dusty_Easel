using System;
using System.Windows.Forms;


namespace Dusty_Easel
{
    public partial class Form1 : Form
    {
        private LoadImage loadImage;

        private Drawing drawing;




        public Form1()
        {
            InitializeComponent();
            loadImage = new LoadImage();
            drawing = new Drawing(easel);
        }

        private void easel_MouseDown(object sender, MouseEventArgs e)
        {
            drawing.easel_MouseDown(e);
        }

        private void easel_MouseUp(object sender, MouseEventArgs e)
        {
            drawing.easel_MouseUp(e);
        }

        private void easel_MouseMove(object sender, MouseEventArgs e)
        {
            drawing.easel_MouseMove(e);
        }

        private void Palette_MouseClick(object sender, MouseEventArgs e)
        {
            drawing.Palette_MouseClick(e, Palette, ColorNow);
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            loadImage.loadImage(Palette);
        }

        private void pencilButton_MouseDown(object sender, MouseEventArgs e)
        {
            drawing.pencilButton_MouseDown();
        }

        private void eraserButton_MouseDown(object sender, MouseEventArgs e)
        {
            drawing.eraserButton_MouseDown();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                pencilButton_MouseDown(pencilButton, null);
            }
            else if (e.KeyCode == Keys.E)
            {
                eraserButton_MouseDown(eraserButton, null);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                drawing.SaveAsPNG(filePath);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                drawing.OpenImage(filePath);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBitmap.Visible = true;
            Main.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.Visible = true;
            drawing.NewBitmap(Convert.ToInt32(Height.Text), Convert.ToInt32(Width.Text));
            NewBitmap.Visible = false;
        }

        private void Palette_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                drawing.changeScale(1.1f);
            }
            if (e.KeyCode == Keys.Subtract)
            {
                drawing.changeScale(0.9f);

            }
        }
    }
}
