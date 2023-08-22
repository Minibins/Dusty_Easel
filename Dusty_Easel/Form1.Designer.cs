namespace Dusty_Easel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.easel = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Palette = new System.Windows.Forms.PictureBox();
            this.ColorNow = new System.Windows.Forms.PictureBox();
            this.loadPalette = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.eraserButton = new System.Windows.Forms.Button();
            this.pencilButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewBitmap = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Height = new System.Windows.Forms.TextBox();
            this.Width = new System.Windows.Forms.TextBox();
            this.Main = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.easel)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Palette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorNow)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.NewBitmap.SuspendLayout();
            this.Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // easel
            // 
            this.easel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.easel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easel.Location = new System.Drawing.Point(157, 2);
            this.easel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.easel.Name = "easel";
            this.easel.Size = new System.Drawing.Size(505, 430);
            this.easel.TabIndex = 0;
            this.easel.TabStop = false;
            this.easel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.easel_MouseDown);
            this.easel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.easel_MouseMove);
            this.easel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.easel_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Palette);
            this.panel1.Controls.Add(this.ColorNow);
            this.panel1.Controls.Add(this.loadPalette);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 430);
            this.panel1.TabIndex = 1;
            // 
            // Palette
            // 
            this.Palette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Palette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Palette.Location = new System.Drawing.Point(1, -4);
            this.Palette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Palette.Name = "Palette";
            this.Palette.Size = new System.Drawing.Size(149, 335);
            this.Palette.TabIndex = 2;
            this.Palette.TabStop = false;
            this.Palette.Click += new System.EventHandler(this.Palette_Click);
            this.Palette.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Palette_MouseClick);
            // 
            // ColorNow
            // 
            this.ColorNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorNow.Location = new System.Drawing.Point(9, 339);
            this.ColorNow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ColorNow.Name = "ColorNow";
            this.ColorNow.Size = new System.Drawing.Size(126, 36);
            this.ColorNow.TabIndex = 1;
            this.ColorNow.TabStop = false;
            // 
            // loadPalette
            // 
            this.loadPalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadPalette.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadPalette.Location = new System.Drawing.Point(9, 382);
            this.loadPalette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadPalette.Name = "loadPalette";
            this.loadPalette.Size = new System.Drawing.Size(125, 38);
            this.loadPalette.TabIndex = 0;
            this.loadPalette.Text = "LoadPalette";
            this.loadPalette.UseVisualStyleBackColor = true;
            this.loadPalette.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.eraserButton);
            this.panel2.Controls.Add(this.pencilButton);
            this.panel2.Location = new System.Drawing.Point(668, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(129, 430);
            this.panel2.TabIndex = 2;
            // 
            // eraserButton
            // 
            this.eraserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eraserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.eraserButton.Location = new System.Drawing.Point(11, 59);
            this.eraserButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eraserButton.Name = "eraserButton";
            this.eraserButton.Size = new System.Drawing.Size(115, 39);
            this.eraserButton.TabIndex = 1;
            this.eraserButton.Text = "eraser";
            this.eraserButton.UseVisualStyleBackColor = true;
            this.eraserButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eraserButton_MouseDown);
            // 
            // pencilButton
            // 
            this.pencilButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pencilButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pencilButton.Location = new System.Drawing.Point(11, 14);
            this.pencilButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pencilButton.Name = "pencilButton";
            this.pencilButton.Size = new System.Drawing.Size(115, 39);
            this.pencilButton.TabIndex = 0;
            this.pencilButton.Text = "pencil";
            this.pencilButton.UseVisualStyleBackColor = true;
            this.pencilButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pencilButton_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // NewBitmap
            // 
            this.NewBitmap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewBitmap.Controls.Add(this.button1);
            this.NewBitmap.Controls.Add(this.Height);
            this.NewBitmap.Controls.Add(this.Width);
            this.NewBitmap.Location = new System.Drawing.Point(269, 169);
            this.NewBitmap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NewBitmap.Name = "NewBitmap";
            this.NewBitmap.Size = new System.Drawing.Size(237, 155);
            this.NewBitmap.TabIndex = 4;
            this.NewBitmap.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 100);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Height
            // 
            this.Height.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Height.Location = new System.Drawing.Point(52, 57);
            this.Height.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(133, 22);
            this.Height.TabIndex = 1;
            // 
            // Width
            // 
            this.Width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Width.Location = new System.Drawing.Point(52, 25);
            this.Width.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(133, 22);
            this.Width.TabIndex = 0;
            // 
            // Main
            // 
            this.Main.Controls.Add(this.panel2);
            this.Main.Controls.Add(this.panel1);
            this.Main.Controls.Add(this.easel);
            this.Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main.Location = new System.Drawing.Point(0, 28);
            this.Main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Main.Name = "Main";
            this.Main.Size = new System.Drawing.Size(800, 422);
            this.Main.TabIndex = 5;
            this.Main.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NewBitmap);
            this.Controls.Add(this.Main);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            ((System.ComponentModel.ISupportInitialize)(this.easel)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Palette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorNow)).EndInit();
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.NewBitmap.ResumeLayout(false);
            this.NewBitmap.PerformLayout();
            this.Main.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox easel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Palette;
        private System.Windows.Forms.PictureBox ColorNow;
        private System.Windows.Forms.Button loadPalette;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button eraserButton;
        private System.Windows.Forms.Button pencilButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel NewBitmap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Height;
        private System.Windows.Forms.TextBox Width;
        private System.Windows.Forms.Panel Main;
    }
}

