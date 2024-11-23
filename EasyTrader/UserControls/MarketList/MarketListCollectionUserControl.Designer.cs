namespace EasyTrader.UserControls.Collections
{
    partial class MarketListCollectionUserControl
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
            Pager pager1 = new Pager();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelTop = new TableLayoutPanel();
            labelTitle = new Label();
            textBoxFilter = new TextBox();
            actionUserControlFilterSearch = new ActionButtons.ActionUserControl();
            actionUserControlFilterClear = new ActionButtons.ActionUserControl();
            tableLayoutPanelPager = new TableLayoutPanel();
            pagerUserControl1 = new PagerUserControl();
            tableLayoutPanelContent = new TableLayoutPanel();
            tableLayoutPanelBottom = new TableLayoutPanel();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelTop.SuspendLayout();
            tableLayoutPanelPager.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.AutoScrollMargin = new Size(10, 10);
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelTop, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelPager, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelContent, 0, 2);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelBottom, 0, 3);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 4;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelMain.Size = new Size(348, 446);
            tableLayoutPanelMain.TabIndex = 1;
            // 
            // tableLayoutPanelTop
            // 
            tableLayoutPanelTop.BackColor = Color.DarkOrange;
            tableLayoutPanelTop.ColumnCount = 4;
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanelTop.Controls.Add(labelTitle, 0, 0);
            tableLayoutPanelTop.Controls.Add(textBoxFilter, 1, 0);
            tableLayoutPanelTop.Controls.Add(actionUserControlFilterSearch, 2, 0);
            tableLayoutPanelTop.Controls.Add(actionUserControlFilterClear, 3, 0);
            tableLayoutPanelTop.Dock = DockStyle.Top;
            tableLayoutPanelTop.Location = new Point(0, 0);
            tableLayoutPanelTop.Margin = new Padding(0);
            tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            tableLayoutPanelTop.RowCount = 1;
            tableLayoutPanelTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelTop.Size = new Size(348, 38);
            tableLayoutPanelTop.TabIndex = 4;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Dock = DockStyle.Top;
            labelTitle.Location = new Point(3, 9);
            labelTitle.Margin = new Padding(3, 9, 3, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(71, 20);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "labelTitle";
            // 
            // textBoxFilter
            // 
            textBoxFilter.Dock = DockStyle.Top;
            textBoxFilter.Location = new Point(77, 5);
            textBoxFilter.Margin = new Padding(0, 5, 5, 0);
            textBoxFilter.Name = "textBoxFilter";
            textBoxFilter.Size = new Size(206, 27);
            textBoxFilter.TabIndex = 1;
            // 
            // actionUserControlFilterSearch
            // 
            actionUserControlFilterSearch.Action = null;
            actionUserControlFilterSearch.Dock = DockStyle.Top;
            actionUserControlFilterSearch.ImageOff = Properties.Resources.Search_40;
            actionUserControlFilterSearch.ImageOn = Properties.Resources.Search_on;
            actionUserControlFilterSearch.ImagePanel = null;
            actionUserControlFilterSearch.Location = new Point(291, 5);
            actionUserControlFilterSearch.Margin = new Padding(3, 5, 3, 3);
            actionUserControlFilterSearch.Name = "actionUserControlFilterSearch";
            actionUserControlFilterSearch.Size = new Size(24, 28);
            actionUserControlFilterSearch.TabIndex = 2;
            // 
            // actionUserControlFilterClear
            // 
            actionUserControlFilterClear.Action = null;
            actionUserControlFilterClear.Dock = DockStyle.Top;
            actionUserControlFilterClear.ImageOff = Properties.Resources.Close1_40;
            actionUserControlFilterClear.ImageOn = Properties.Resources.Close1_on;
            actionUserControlFilterClear.ImagePanel = null;
            actionUserControlFilterClear.Location = new Point(321, 5);
            actionUserControlFilterClear.Margin = new Padding(3, 5, 3, 3);
            actionUserControlFilterClear.Name = "actionUserControlFilterClear";
            actionUserControlFilterClear.Size = new Size(24, 28);
            actionUserControlFilterClear.TabIndex = 3;
            // 
            // tableLayoutPanelPager
            // 
            tableLayoutPanelPager.BackColor = SystemColors.AppWorkspace;
            tableLayoutPanelPager.ColumnCount = 1;
            tableLayoutPanelPager.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelPager.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelPager.Controls.Add(pagerUserControl1, 0, 0);
            tableLayoutPanelPager.Dock = DockStyle.Top;
            tableLayoutPanelPager.Location = new Point(0, 38);
            tableLayoutPanelPager.Margin = new Padding(0);
            tableLayoutPanelPager.Name = "tableLayoutPanelPager";
            tableLayoutPanelPager.RowCount = 1;
            tableLayoutPanelPager.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelPager.Size = new Size(348, 30);
            tableLayoutPanelPager.TabIndex = 3;
            // 
            // pagerUserControl1
            // 
            pagerUserControl1.BackColor = Color.Transparent;
            pagerUserControl1.Dock = DockStyle.Top;
            pagerUserControl1.Location = new Point(0, 0);
            pagerUserControl1.Margin = new Padding(0);
            pagerUserControl1.Name = "pagerUserControl1";
            pager1.AllItems = null;
            pager1.CurrentIndex = 0;
            pagerUserControl1.Pager = pager1;
            pagerUserControl1.Size = new Size(348, 30);
            pagerUserControl1.TabIndex = 2;
            // 
            // tableLayoutPanelContent
            // 
            tableLayoutPanelContent.AutoScroll = true;
            tableLayoutPanelContent.AutoScrollMargin = new Size(10, 10);
            tableLayoutPanelContent.BackColor = Color.White;
            tableLayoutPanelContent.ColumnCount = 1;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelContent.Dock = DockStyle.Fill;
            tableLayoutPanelContent.Location = new Point(3, 71);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.RowCount = 1;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Size = new Size(342, 342);
            tableLayoutPanelContent.TabIndex = 1;
            // 
            // tableLayoutPanelBottom
            // 
            tableLayoutPanelBottom.BackColor = Color.DarkOrange;
            tableLayoutPanelBottom.ColumnCount = 1;
            tableLayoutPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelBottom.Dock = DockStyle.Bottom;
            tableLayoutPanelBottom.Location = new Point(0, 416);
            tableLayoutPanelBottom.Margin = new Padding(0);
            tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            tableLayoutPanelBottom.RowCount = 1;
            tableLayoutPanelBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelBottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelBottom.Size = new Size(348, 30);
            tableLayoutPanelBottom.TabIndex = 5;
            // 
            // MarketListCollectionUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 128, 0);
            Controls.Add(tableLayoutPanelMain);
            Name = "MarketListCollectionUserControl";
            NumberOfPageItems = 50;
            Size = new Size(348, 446);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelTop.ResumeLayout(false);
            tableLayoutPanelTop.PerformLayout();
            tableLayoutPanelPager.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelPager;
        private PagerUserControl pagerUserControl1;
        private TableLayoutPanel tableLayoutPanelTop;
        private Label labelTitle;
        private TextBox textBoxFilter;
        private TableLayoutPanel tableLayoutPanelBottom;
        private ActionButtons.ActionUserControl actionUserControlFilterSearch;
        private ActionButtons.ActionUserControl actionUserControlFilterClear;
        private TableLayoutPanel tableLayoutPanelContent;
    }
}
