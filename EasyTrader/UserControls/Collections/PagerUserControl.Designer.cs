namespace EasyTrader.UserControls.Collections
{
    partial class PagerUserControl
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
            nextUserControl = new ActionButtons.ActionUserControl();
            previousUserControl = new ActionButtons.ActionUserControl();
            labelPager = new Label();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.Transparent;
            tableLayoutPanelMain.ColumnCount = 3;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            tableLayoutPanelMain.Controls.Add(nextUserControl, 2, 0);
            tableLayoutPanelMain.Controls.Add(previousUserControl, 0, 0);
            tableLayoutPanelMain.Controls.Add(labelPager, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Top;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(255, 30);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // nextUserControl
            // 
            nextUserControl.Action = null;
            nextUserControl.Dock = DockStyle.Left;
            nextUserControl.ImageOff = Properties.Resources.Next1_40;
            nextUserControl.ImageOn = Properties.Resources.Next1_On;
            nextUserControl.ImagePanel = null;
            nextUserControl.Location = new Point(220, 5);
            nextUserControl.Margin = new Padding(0, 5, 5, 5);
            nextUserControl.Name = "nextUserControl";
            nextUserControl.Size = new Size(25, 20);
            nextUserControl.TabIndex = 1;
            // 
            // previousUserControl
            // 
            previousUserControl.Action = null;
            previousUserControl.Dock = DockStyle.Right;
            previousUserControl.ImageOff = Properties.Resources.Previous1_40;
            previousUserControl.ImageOn = Properties.Resources.Previous1_On;
            previousUserControl.ImagePanel = null;
            previousUserControl.Location = new Point(10, 5);
            previousUserControl.Margin = new Padding(5, 5, 0, 5);
            previousUserControl.Name = "previousUserControl";
            previousUserControl.Size = new Size(25, 20);
            previousUserControl.TabIndex = 0;
            // 
            // labelPager
            // 
            labelPager.AutoSize = true;
            labelPager.BackColor = Color.Transparent;
            labelPager.Dock = DockStyle.Top;
            labelPager.Location = new Point(35, 6);
            labelPager.Margin = new Padding(0, 6, 0, 0);
            labelPager.Name = "labelPager";
            labelPager.Size = new Size(185, 20);
            labelPager.TabIndex = 2;
            labelPager.Text = "label1";
            labelPager.TextAlign = ContentAlignment.TopCenter;
            // 
            // PagerUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            Name = "PagerUserControl";
            Size = new Size(255, 30);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private ActionButtons.ActionUserControl previousUserControl;
        private ActionButtons.ActionUserControl nextUserControl;
        private Label labelPager;
    }
}
