namespace EasyTrader.UserControls
{
    partial class PropertyDataUserControl
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
            Collections.Pager pager1 = new Collections.Pager();
            labelTitle = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelContent = new TableLayoutPanel();
            panelLeftRuller = new Panel();
            panelRuler = new Panel();
            tableLayoutPanelData = new TableLayoutPanel();
            panelProperties = new Panel();
            panelObjets = new Panel();
            pagerUserControl1 = new Collections.PagerUserControl();
            tableLayoutPanelTop = new TableLayoutPanel();
            openCloseUserControl = new ActionButtons.ActionUserControl();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelContent.SuspendLayout();
            panelLeftRuller.SuspendLayout();
            tableLayoutPanelData.SuspendLayout();
            tableLayoutPanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Location = new Point(30, 4);
            labelTitle.Margin = new Padding(0, 4, 3, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(50, 20);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "label1";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.Transparent;
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelContent, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelTop, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Top;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.Size = new Size(687, 556);
            tableLayoutPanelMain.TabIndex = 1;
            // 
            // tableLayoutPanelContent
            // 
            tableLayoutPanelContent.BackColor = Color.Transparent;
            tableLayoutPanelContent.ColumnCount = 2;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Controls.Add(panelLeftRuller, 0, 0);
            tableLayoutPanelContent.Controls.Add(tableLayoutPanelData, 1, 0);
            tableLayoutPanelContent.Dock = DockStyle.Top;
            tableLayoutPanelContent.Location = new Point(0, 30);
            tableLayoutPanelContent.Margin = new Padding(0);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.RowCount = 1;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle());
            tableLayoutPanelContent.Size = new Size(687, 418);
            tableLayoutPanelContent.TabIndex = 1;
            // 
            // panelLeftRuller
            // 
            panelLeftRuller.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            panelLeftRuller.BackColor = Color.Transparent;
            panelLeftRuller.Controls.Add(panelRuler);
            panelLeftRuller.Location = new Point(0, 10);
            panelLeftRuller.Margin = new Padding(0, 10, 3, 10);
            panelLeftRuller.Name = "panelLeftRuller";
            panelLeftRuller.Size = new Size(25, 398);
            panelLeftRuller.TabIndex = 7;
            // 
            // panelRuler
            // 
            panelRuler.BackColor = Color.Silver;
            panelRuler.Dock = DockStyle.Right;
            panelRuler.Location = new Point(19, 0);
            panelRuler.Margin = new Padding(0);
            panelRuler.Name = "panelRuler";
            panelRuler.Size = new Size(6, 398);
            panelRuler.TabIndex = 4;
            // 
            // tableLayoutPanelData
            // 
            tableLayoutPanelData.BackColor = Color.Transparent;
            tableLayoutPanelData.ColumnCount = 1;
            tableLayoutPanelData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.Controls.Add(panelProperties, 0, 0);
            tableLayoutPanelData.Controls.Add(panelObjets, 0, 1);
            tableLayoutPanelData.Controls.Add(pagerUserControl1, 0, 2);
            tableLayoutPanelData.Dock = DockStyle.Top;
            tableLayoutPanelData.Location = new Point(30, 8);
            tableLayoutPanelData.Margin = new Padding(2, 8, 10, 10);
            tableLayoutPanelData.Name = "tableLayoutPanelData";
            tableLayoutPanelData.RowCount = 3;
            tableLayoutPanelData.RowStyles.Add(new RowStyle());
            tableLayoutPanelData.RowStyles.Add(new RowStyle());
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelData.Size = new Size(647, 363);
            tableLayoutPanelData.TabIndex = 5;
            // 
            // panelProperties
            // 
            panelProperties.BackColor = Color.Transparent;
            panelProperties.Dock = DockStyle.Top;
            panelProperties.Location = new Point(0, 0);
            panelProperties.Margin = new Padding(0);
            panelProperties.Name = "panelProperties";
            panelProperties.Size = new Size(647, 16);
            panelProperties.TabIndex = 0;
            // 
            // panelObjets
            // 
            panelObjets.BackColor = Color.Transparent;
            panelObjets.Dock = DockStyle.Top;
            panelObjets.Location = new Point(0, 16);
            panelObjets.Margin = new Padding(0);
            panelObjets.Name = "panelObjets";
            panelObjets.Size = new Size(647, 21);
            panelObjets.TabIndex = 1;
            // 
            // pagerUserControl1
            // 
            pagerUserControl1.BackColor = Color.LightGray;
            pagerUserControl1.Dock = DockStyle.Top;
            pagerUserControl1.Location = new Point(0, 37);
            pagerUserControl1.Margin = new Padding(0);
            pagerUserControl1.Name = "pagerUserControl1";
            pager1.AllItems = null;
            pager1.CurrentIndex = 0;
            pagerUserControl1.Pager = pager1;
            pagerUserControl1.Size = new Size(647, 28);
            pagerUserControl1.TabIndex = 2;
            // 
            // tableLayoutPanelTop
            // 
            tableLayoutPanelTop.BackColor = Color.Transparent;
            tableLayoutPanelTop.ColumnCount = 2;
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelTop.Controls.Add(labelTitle, 1, 0);
            tableLayoutPanelTop.Controls.Add(openCloseUserControl, 0, 0);
            tableLayoutPanelTop.Dock = DockStyle.Fill;
            tableLayoutPanelTop.Location = new Point(0, 0);
            tableLayoutPanelTop.Margin = new Padding(0);
            tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            tableLayoutPanelTop.RowCount = 1;
            tableLayoutPanelTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelTop.Size = new Size(687, 30);
            tableLayoutPanelTop.TabIndex = 0;
            // 
            // openCloseUserControl
            // 
            openCloseUserControl.Action = null;
            openCloseUserControl.ImageOff = null;
            openCloseUserControl.ImageOn = null;
            openCloseUserControl.ImagePanel = null;
            openCloseUserControl.Location = new Point(3, 8);
            openCloseUserControl.Margin = new Padding(3, 8, 11, 8);
            openCloseUserControl.Name = "openCloseUserControl";
            openCloseUserControl.Size = new Size(16, 14);
            openCloseUserControl.TabIndex = 1;
            // 
            // PropertyDataUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            Name = "PropertyDataUserControl";
            Size = new Size(687, 679);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelContent.ResumeLayout(false);
            panelLeftRuller.ResumeLayout(false);
            tableLayoutPanelData.ResumeLayout(false);
            tableLayoutPanelTop.ResumeLayout(false);
            tableLayoutPanelTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelTop;
        private TableLayoutPanel tableLayoutPanelContent;
        private TableLayoutPanel tableLayoutPanelData;
        private Label labelTitle;
        private ActionButtons.ActionUserControl openCloseUserControl;
        private Panel panelProperties;
        private Panel panelObjets;
        private Panel panelLeftRuller;
        private Panel panelRuler;
        private Collections.PagerUserControl pagerUserControl1;
    }
}
