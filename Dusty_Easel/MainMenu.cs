using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Dusty_Easel
{
    class MainMenu
    {
     
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
            Background= Features.ApplyZoom(Background, MenuEasel);
            string[] adresses= File.Exists("C: /Users/user/Documents/DustyEasel/LastFiles") ? File.ReadAllLines("C: /Users/user/Documents/DustyEasel/LastFiles") : new string[0];         
            if(adresses.Length>0)Background = Features.ReplaceColorAreaWithImage(Background, new Bitmap(adresses[0]), Color.FromArgb(255, 255, 255));
            else Background = Features.ReplaceColorAreaWithImage(Background, Properties.Resources._1, Color.FromArgb(255, 255, 255));
            if (adresses.Length > 1)Background = Features.ReplaceColorAreaWithImage(Background, new Bitmap(adresses[1]), Color.FromArgb(255, 0, 0));
            else Background = Features.ReplaceColorAreaWithImage(Background, Properties.Resources._3, Color.FromArgb(255, 0, 0));
            if (adresses.Length > 2)Background = Features.ReplaceColorAreaWithImage(Background, new Bitmap(adresses[2]), Color.FromArgb(0, 0, 0));
            else Background = Features.ReplaceColorAreaWithImage(Background, Properties.Resources._2, Color.FromArgb(0, 0, 0));
            MenuEasel.Image = Background;
            
            return;
        }
    }
}
