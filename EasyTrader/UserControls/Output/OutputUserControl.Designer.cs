namespace EasyTrader.UserControls.Output
{
    partial class OutputUserControl
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
            panelControl = new Panel();
            buttonClear = new Button();
            tableLayoutPanelContent = new TableLayoutPanel();
            tableLayoutPanelMain.SuspendLayout();
            panelControl.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.AutoScrollMargin = new Size(10, 10);
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(panelControl, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelContent, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(540, 444);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelControl
            // 
            panelControl.BackColor = Color.FromArgb(255, 128, 0);
            panelControl.Controls.Add(buttonClear);
            panelControl.Dock = DockStyle.Fill;
            panelControl.Location = new Point(0, 0);
            panelControl.Margin = new Padding(0);
            panelControl.Name = "panelControl";
            panelControl.Size = new Size(540, 27);
            panelControl.TabIndex = 0;
            // 
            // buttonClear
            // 
            buttonClear.Dock = DockStyle.Right;
            buttonClear.Location = new Point(471, 0);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(69, 27);
            buttonClear.TabIndex = 0;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelContent
            // 
            tableLayoutPanelContent.AutoScroll = true;
            tableLayoutPanelContent.BackColor = Color.Gainsboro;
            tableLayoutPanelContent.ColumnCount = 1;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelContent.Dock = DockStyle.Fill;
            tableLayoutPanelContent.Location = new Point(3, 30);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.RowCount = 1;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Size = new Size(534, 411);
            tableLayoutPanelContent.TabIndex = 1;
            // 
            // OutputUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            Name = "OutputUserControl";
            Size = new Size(540, 444);
            tableLayoutPanelMain.ResumeLayout(false);
            panelControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private Panel panelControl;
        private Button buttonClear;
        private TableLayoutPanel tableLayoutPanelContent;
    }
}
