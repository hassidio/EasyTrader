namespace EasyTrader.UserControls.ActionButtons
{
    partial class ActionUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelOpenPropertyMain = new Panel();
            pictureBoxOpen = new PictureBox();
            panelOpenPropertyMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOpen).BeginInit();
            SuspendLayout();
            // 
            // panelOpenPropertyMain
            // 
            panelOpenPropertyMain.Controls.Add(pictureBoxOpen);
            panelOpenPropertyMain.Dock = DockStyle.Fill;
            panelOpenPropertyMain.Location = new Point(0, 0);
            panelOpenPropertyMain.Margin = new Padding(0);
            panelOpenPropertyMain.Name = "panelOpenPropertyMain";
            panelOpenPropertyMain.Padding = new Padding(1);
            panelOpenPropertyMain.Size = new Size(28, 28);
            panelOpenPropertyMain.TabIndex = 6;
            // 
            // pictureBoxOpen
            // 
            pictureBoxOpen.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxOpen.Cursor = Cursors.Hand;
            pictureBoxOpen.Dock = DockStyle.Fill;
            pictureBoxOpen.Image = Properties.Resources.Default;
            pictureBoxOpen.Location = new Point(1, 1);
            pictureBoxOpen.Margin = new Padding(0);
            pictureBoxOpen.Name = "pictureBoxOpen";
            pictureBoxOpen.Size = new Size(26, 26);
            pictureBoxOpen.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOpen.TabIndex = 5;
            pictureBoxOpen.TabStop = false;
            pictureBoxOpen.MouseClick += pictureBoxOpen_MouseClick;
            pictureBoxOpen.MouseLeave += pictureBoxOpen_MouseLeave;
            pictureBoxOpen.MouseHover += pictureBoxOpen_MouseHover;
            // 
            // ActionUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelOpenPropertyMain);
            Margin = new Padding(0);
            Name = "ActionUserControl";
            Size = new Size(28, 28);
            panelOpenPropertyMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOpen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelOpenPropertyMain;
        private PictureBox pictureBoxOpen;
    }
}
