namespace EasyTrader
{
    partial class OutputItemUserControl
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
            tableLayoutPanelMain = new TableLayoutPanel();
            labelDateTime = new Label();
            labelText = new Label();
            panelStatus = new Panel();
            pictureBoxStatus = new PictureBox();
            openPropertyButton1 = new UserControls.ActionButtons.OpenPropertyButton();
            tableLayoutPanelMain.SuspendLayout();
            panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatus).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.AutoSize = true;
            tableLayoutPanelMain.BackColor = Color.White;
            tableLayoutPanelMain.ColumnCount = 4;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelMain.Controls.Add(labelDateTime, 0, 0);
            tableLayoutPanelMain.Controls.Add(labelText, 2, 0);
            tableLayoutPanelMain.Controls.Add(panelStatus, 1, 0);
            tableLayoutPanelMain.Controls.Add(openPropertyButton1, 3, 0);
            tableLayoutPanelMain.Dock = DockStyle.Top;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.Size = new Size(667, 28);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // labelDateTime
            // 
            labelDateTime.AutoSize = true;
            labelDateTime.BackColor = Color.White;
            labelDateTime.Dock = DockStyle.Top;
            labelDateTime.Location = new Point(0, 0);
            labelDateTime.Margin = new Padding(0);
            labelDateTime.Name = "labelDateTime";
            labelDateTime.Padding = new Padding(4);
            labelDateTime.Size = new Size(115, 28);
            labelDateTime.TabIndex = 0;
            labelDateTime.Text = "lableDateTime";
            // 
            // labelText
            // 
            labelText.AutoSize = true;
            labelText.BackColor = Color.White;
            labelText.Dock = DockStyle.Top;
            labelText.Location = new Point(143, 0);
            labelText.Margin = new Padding(0);
            labelText.MinimumSize = new Size(0, 28);
            labelText.Name = "labelText";
            labelText.Padding = new Padding(4);
            labelText.Size = new Size(486, 28);
            labelText.TabIndex = 2;
            labelText.Text = "labelText";
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.White;
            panelStatus.Controls.Add(pictureBoxStatus);
            panelStatus.Dock = DockStyle.Top;
            panelStatus.Location = new Point(115, 0);
            panelStatus.Margin = new Padding(0);
            panelStatus.Name = "panelStatus";
            panelStatus.Padding = new Padding(3);
            panelStatus.Size = new Size(28, 28);
            panelStatus.TabIndex = 3;
            // 
            // pictureBoxStatus
            // 
            pictureBoxStatus.Dock = DockStyle.Fill;
            pictureBoxStatus.Image = Properties.Resources.InProgress1;
            pictureBoxStatus.Location = new Point(3, 3);
            pictureBoxStatus.Margin = new Padding(0);
            pictureBoxStatus.Name = "pictureBoxStatus";
            pictureBoxStatus.Size = new Size(22, 22);
            pictureBoxStatus.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxStatus.TabIndex = 2;
            pictureBoxStatus.TabStop = false;
            // 
            // openPropertyButton1
            // 
            openPropertyButton1.Dock = DockStyle.Top;
            openPropertyButton1.Location = new Point(629, 0);
            openPropertyButton1.Margin = new Padding(0, 0, 10, 0);
            openPropertyButton1.Name = "openPropertyButton1";
            openPropertyButton1.Size = new Size(28, 28);
            openPropertyButton1.TabIndex = 4;
            // 
            // OutputItemUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            Name = "OutputItemUserControl";
            Size = new Size(667, 30);
            ClientSizeChanged += OutputItemUserControl_ClientSizeChanged;
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            panelStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatus).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private Label labelDateTime;
        private Label labelText;
        private Panel panelStatus;
        private PictureBox pictureBoxStatus;
        private UserControls.ActionButtons.OpenPropertyButton openPropertyButton1;
    }
}
