using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Dusty_Easel
{
    class MainMenu
    {
        private GraphicFeatures gr=new GraphicFeatures();
        private PictureBox MenuEasel;
        private Bitmap Background1 = Properties.Resources.background;
        private Bitmap Background2 = Properties.Resources.easel;
        private Bitmap Background3 = Properties.Resources.Paintinss;
        public MainMenu(PictureBox menu)
        {
            MenuEasel = menu;
        }
        public void MenuBackground(MouseEventArgs e)
        {
            Bitmap Background = new Bitmap(80, 45);
            Graphics g = Graphics.FromImage(Background);
            g.DrawImage(Background1,0,0);
            g.DrawImage(Background2, 0 + e.X*5/MenuEasel.Width, 0);
            g.DrawImage(Background3, 0 + e.X * 2 / MenuEasel.Width, 0);
            Background= gr.ApplyZoom(Background, MenuEasel);
            Background = gr.ReplaceAreaWithImage(Background, new Bitmap("C:/Users/user/Desktop/Aseprite/data/Я поддерживаю демона пиццу.png"), Color.White);
            MenuEasel.Image = Background;
            
            return;
        }
    }
}
