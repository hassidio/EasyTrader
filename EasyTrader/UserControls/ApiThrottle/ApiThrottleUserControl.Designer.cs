using EasyTrader.UserControls.PropertyBag;

namespace EasyTrader.UserControls.ApiThrottle
{
    partial class ApiThrottleUserControl
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
            throttlePropertyBagUserControl = new PropertyBagUserControl();
            tableLayoutPanelIndicator = new TableLayoutPanel();
            panelRed = new Panel();
            panelGreen = new Panel();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelIndicator.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(throttlePropertyBagUserControl, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelIndicator, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(3);
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(482, 448);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // throttlePropertyBagUserControl
            // 
            throttlePropertyBagUserControl.BackColor = Color.RosyBrown;
            throttlePropertyBagUserControl.Dock = DockStyle.Fill;
            throttlePropertyBagUserControl.Location = new Point(6, 21);
            throttlePropertyBagUserControl.MinimumSize = new Size(300, 27);
            throttlePropertyBagUserControl.Name = "throttlePropertyBagUserControl";
            throttlePropertyBagUserControl.Size = new Size(470, 421);
            throttlePropertyBagUserControl.TabIndex = 1;
            // 
            // tableLayoutPanelIndicator
            // 
            tableLayoutPanelIndicator.BackColor = Color.Olive;
            tableLayoutPanelIndicator.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanelIndicator.ColumnCount = 2;
            tableLayoutPanelIndicator.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            tableLayoutPanelIndicator.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99F));
            tableLayoutPanelIndicator.Controls.Add(panelRed, 0, 0);
            tableLayoutPanelIndicator.Controls.Add(panelGreen, 1, 0);
            tableLayoutPanelIndicator.Dock = DockStyle.Fill;
            tableLayoutPanelIndicator.Location = new Point(3, 3);
            tableLayoutPanelIndicator.Margin = new Padding(0);
            tableLayoutPanelIndicator.Name = "tableLayoutPanelIndicator";
            tableLayoutPanelIndicator.RowCount = 1;
            tableLayoutPanelIndicator.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelIndicator.Size = new Size(476, 15);
            tableLayoutPanelIndicator.TabIndex = 0;
            // 
            // panelRed
            // 
            panelRed.BackColor = Color.FromArgb(192, 0, 0);
            panelRed.Dock = DockStyle.Fill;
            panelRed.Location = new Point(1, 1);
            panelRed.Margin = new Padding(0);
            panelRed.Name = "panelRed";
            panelRed.Size = new Size(4, 13);
            panelRed.TabIndex = 0;
            // 
            // panelGreen
            // 
            panelGreen.BackColor = Color.Green;
            panelGreen.Dock = DockStyle.Fill;
            panelGreen.Location = new Point(6, 1);
            panelGreen.Margin = new Padding(0);
            panelGreen.Name = "panelGreen";
            panelGreen.Size = new Size(469, 13);
            panelGreen.TabIndex = 1;
            // 
            // ApiThrottleUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Name = "ApiThrottleUserControl";
            Size = new Size(482, 448);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelIndicator.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelIndicator;
        private Panel panelRed;
        private Panel panelGreen;
        private Panel panelData;
        private PropertyBagUserControl throttlePropertyBagUserControl;
    }
}
