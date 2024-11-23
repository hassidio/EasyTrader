namespace EasyTrader.Forms
{
    partial class Main
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
            splitContainerLeftP1 = new SplitContainer();
            tableLayoutPanelLeft = new TableLayoutPanel();
            tableLayoutPanelExchange = new TableLayoutPanel();
            labelExchange = new Label();
            comboBoxExchange = new ComboBox();
            marketListUserControl = new UserControls.Collections.MarketListCollectionUserControl();
            splitContainerRightP2 = new SplitContainer();
            splitContainerCentre = new SplitContainer();
            buttonTestOutputWindow = new Button();
            buttonTestOutput = new Button();
            buttonError = new Button();
            buttonForm3 = new Button();
            buttonTestBinanceOutput = new Button();
            buttonForm2 = new Button();
            outputUserControl1 = new UserControls.Output.OutputUserControl();
            tableLayoutPanelRight = new TableLayoutPanel();
            tabControlApiThrottle = new TabControl();
            ((System.ComponentModel.ISupportInitialize)splitContainerLeftP1).BeginInit();
            splitContainerLeftP1.Panel1.SuspendLayout();
            splitContainerLeftP1.Panel2.SuspendLayout();
            splitContainerLeftP1.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            tableLayoutPanelExchange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerRightP2).BeginInit();
            splitContainerRightP2.Panel1.SuspendLayout();
            splitContainerRightP2.Panel2.SuspendLayout();
            splitContainerRightP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerCentre).BeginInit();
            splitContainerCentre.Panel1.SuspendLayout();
            splitContainerCentre.Panel2.SuspendLayout();
            splitContainerCentre.SuspendLayout();
            tableLayoutPanelRight.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerLeftP1
            // 
            splitContainerLeftP1.BackColor = Color.DarkOrange;
            splitContainerLeftP1.Dock = DockStyle.Fill;
            splitContainerLeftP1.Location = new Point(0, 0);
            splitContainerLeftP1.Name = "splitContainerLeftP1";
            // 
            // splitContainerLeftP1.Panel1
            // 
            splitContainerLeftP1.Panel1.BackColor = Color.Linen;
            splitContainerLeftP1.Panel1.Controls.Add(tableLayoutPanelLeft);
            // 
            // splitContainerLeftP1.Panel2
            // 
            splitContainerLeftP1.Panel2.Controls.Add(splitContainerRightP2);
            splitContainerLeftP1.Size = new Size(1230, 796);
            splitContainerLeftP1.SplitterDistance = 308;
            splitContainerLeftP1.TabIndex = 0;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.BackColor = Color.DarkOrange;
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(tableLayoutPanelExchange, 0, 0);
            tableLayoutPanelLeft.Controls.Add(marketListUserControl, 0, 1);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(0, 0);
            tableLayoutPanelLeft.Margin = new Padding(0);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.RowCount = 2;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Size = new Size(308, 796);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // tableLayoutPanelExchange
            // 
            tableLayoutPanelExchange.BackColor = Color.DarkOrange;
            tableLayoutPanelExchange.ColumnCount = 1;
            tableLayoutPanelExchange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelExchange.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelExchange.Controls.Add(labelExchange, 0, 0);
            tableLayoutPanelExchange.Controls.Add(comboBoxExchange, 0, 1);
            tableLayoutPanelExchange.Dock = DockStyle.Fill;
            tableLayoutPanelExchange.Location = new Point(0, 0);
            tableLayoutPanelExchange.Margin = new Padding(0);
            tableLayoutPanelExchange.Name = "tableLayoutPanelExchange";
            tableLayoutPanelExchange.Padding = new Padding(5);
            tableLayoutPanelExchange.RowCount = 2;
            tableLayoutPanelExchange.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelExchange.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelExchange.Size = new Size(308, 72);
            tableLayoutPanelExchange.TabIndex = 0;
            // 
            // labelExchange
            // 
            labelExchange.AutoSize = true;
            labelExchange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelExchange.Location = new Point(8, 5);
            labelExchange.Name = "labelExchange";
            labelExchange.Padding = new Padding(4);
            labelExchange.Size = new Size(87, 28);
            labelExchange.TabIndex = 0;
            labelExchange.Text = "Exchange:";
            // 
            // comboBoxExchange
            // 
            comboBoxExchange.Dock = DockStyle.Top;
            comboBoxExchange.FormattingEnabled = true;
            comboBoxExchange.Location = new Point(8, 39);
            comboBoxExchange.Name = "comboBoxExchange";
            comboBoxExchange.Size = new Size(292, 28);
            comboBoxExchange.TabIndex = 1;
            comboBoxExchange.SelectedIndexChanged += comboBoxExchange_SelectedIndexChanged;
            // 
            // marketListUserControl
            // 
            marketListUserControl.BackColor = Color.FromArgb(255, 128, 0);
            marketListUserControl.CollectionData = null;
            marketListUserControl.Dock = DockStyle.Fill;
            marketListUserControl.ExchangeDataManager = null;
            marketListUserControl.LazyLoad = false;
            marketListUserControl.Location = new Point(5, 72);
            marketListUserControl.Margin = new Padding(5, 0, 0, 0);
            marketListUserControl.Name = "marketListUserControl";
            marketListUserControl.NumberOfPageItems = 5;
            marketListUserControl.ParentControl = null;
            marketListUserControl.PropertyDataObject = null;
            marketListUserControl.Size = new Size(303, 724);
            marketListUserControl.TabIndex = 1;
            // 
            // splitContainerRightP2
            // 
            splitContainerRightP2.BackColor = Color.DarkOrange;
            splitContainerRightP2.Dock = DockStyle.Fill;
            splitContainerRightP2.Location = new Point(0, 0);
            splitContainerRightP2.Name = "splitContainerRightP2";
            // 
            // splitContainerRightP2.Panel1
            // 
            splitContainerRightP2.Panel1.Controls.Add(splitContainerCentre);
            // 
            // splitContainerRightP2.Panel2
            // 
            splitContainerRightP2.Panel2.BackColor = Color.Linen;
            splitContainerRightP2.Panel2.Controls.Add(tableLayoutPanelRight);
            splitContainerRightP2.Size = new Size(918, 796);
            splitContainerRightP2.SplitterDistance = 745;
            splitContainerRightP2.TabIndex = 0;
            // 
            // splitContainerCentre
            // 
            splitContainerCentre.BackColor = Color.DarkOrange;
            splitContainerCentre.Dock = DockStyle.Fill;
            splitContainerCentre.Location = new Point(0, 0);
            splitContainerCentre.Margin = new Padding(0);
            splitContainerCentre.Name = "splitContainerCentre";
            splitContainerCentre.Orientation = Orientation.Horizontal;
            // 
            // splitContainerCentre.Panel1
            // 
            splitContainerCentre.Panel1.BackColor = Color.SeaShell;
            splitContainerCentre.Panel1.Controls.Add(buttonTestOutputWindow);
            splitContainerCentre.Panel1.Controls.Add(buttonTestOutput);
            splitContainerCentre.Panel1.Controls.Add(buttonError);
            splitContainerCentre.Panel1.Controls.Add(buttonForm3);
            splitContainerCentre.Panel1.Controls.Add(buttonTestBinanceOutput);
            splitContainerCentre.Panel1.Controls.Add(buttonForm2);
            // 
            // splitContainerCentre.Panel2
            // 
            splitContainerCentre.Panel2.AutoScroll = true;
            splitContainerCentre.Panel2.Controls.Add(outputUserControl1);
            splitContainerCentre.Size = new Size(745, 796);
            splitContainerCentre.SplitterDistance = 573;
            splitContainerCentre.TabIndex = 0;
            // 
            // buttonTestOutputWindow
            // 
            buttonTestOutputWindow.Location = new Point(445, 143);
            buttonTestOutputWindow.Name = "buttonTestOutputWindow";
            buttonTestOutputWindow.Size = new Size(170, 29);
            buttonTestOutputWindow.TabIndex = 5;
            buttonTestOutputWindow.Text = "Test Output Window";
            buttonTestOutputWindow.UseVisualStyleBackColor = true;
            buttonTestOutputWindow.Click += buttonTestOutputWindow_Click;
            // 
            // buttonTestOutput
            // 
            buttonTestOutput.Location = new Point(441, 5);
            buttonTestOutput.Name = "buttonTestOutput";
            buttonTestOutput.Size = new Size(174, 29);
            buttonTestOutput.TabIndex = 1;
            buttonTestOutput.Text = "Test Output";
            buttonTestOutput.UseVisualStyleBackColor = true;
            buttonTestOutput.Click += buttonTestOutput_Click;
            // 
            // buttonError
            // 
            buttonError.Location = new Point(441, 96);
            buttonError.Name = "buttonError";
            buttonError.Size = new Size(174, 29);
            buttonError.TabIndex = 4;
            buttonError.Text = "Test Error";
            buttonError.UseVisualStyleBackColor = true;
            buttonError.Click += buttonError_Click;
            // 
            // buttonForm3
            // 
            buttonForm3.Location = new Point(227, 47);
            buttonForm3.Name = "buttonForm3";
            buttonForm3.Size = new Size(161, 29);
            buttonForm3.TabIndex = 3;
            buttonForm3.Text = "Form 3";
            buttonForm3.UseVisualStyleBackColor = true;
            buttonForm3.Click += buttonForm3_Click;
            // 
            // buttonTestBinanceOutput
            // 
            buttonTestBinanceOutput.Location = new Point(441, 47);
            buttonTestBinanceOutput.Name = "buttonTestBinanceOutput";
            buttonTestBinanceOutput.Size = new Size(174, 29);
            buttonTestBinanceOutput.TabIndex = 0;
            buttonTestBinanceOutput.Text = "Test Binance Output";
            buttonTestBinanceOutput.UseVisualStyleBackColor = true;
            buttonTestBinanceOutput.Click += buttonTestBinanceOutput_Click;
            // 
            // buttonForm2
            // 
            buttonForm2.Location = new Point(227, 12);
            buttonForm2.Name = "buttonForm2";
            buttonForm2.Size = new Size(163, 29);
            buttonForm2.TabIndex = 2;
            buttonForm2.Text = "Form2";
            buttonForm2.UseVisualStyleBackColor = true;
            buttonForm2.Click += buttonForm2_Click;
            // 
            // outputUserControl1
            // 
            outputUserControl1.BackColor = Color.White;
            outputUserControl1.BorderStyle = BorderStyle.FixedSingle;
            outputUserControl1.Dock = DockStyle.Fill;
            outputUserControl1.LazyLoad = false;
            outputUserControl1.Location = new Point(0, 0);
            outputUserControl1.Margin = new Padding(0);
            outputUserControl1.Name = "outputUserControl1";
            outputUserControl1.ParentControl = null;
            outputUserControl1.PropertyDataObject = null;
            outputUserControl1.Size = new Size(745, 219);
            outputUserControl1.TabIndex = 0;
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(tabControlApiThrottle, 0, 2);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(0, 0);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 3;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 224F));
            tableLayoutPanelRight.Size = new Size(169, 796);
            tableLayoutPanelRight.TabIndex = 2;
            // 
            // tabControlApiThrottle
            // 
            tabControlApiThrottle.Dock = DockStyle.Fill;
            tabControlApiThrottle.Location = new Point(3, 575);
            tabControlApiThrottle.Name = "tabControlApiThrottle";
            tabControlApiThrottle.SelectedIndex = 0;
            tabControlApiThrottle.ShowToolTips = true;
            tabControlApiThrottle.Size = new Size(163, 218);
            tabControlApiThrottle.TabIndex = 0;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 796);
            Controls.Add(splitContainerLeftP1);
            Name = "Main";
            Text = "Main";
            WindowState = FormWindowState.Maximized;
            Load += Main_Load;
            splitContainerLeftP1.Panel1.ResumeLayout(false);
            splitContainerLeftP1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerLeftP1).EndInit();
            splitContainerLeftP1.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            tableLayoutPanelExchange.ResumeLayout(false);
            tableLayoutPanelExchange.PerformLayout();
            splitContainerRightP2.Panel1.ResumeLayout(false);
            splitContainerRightP2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerRightP2).EndInit();
            splitContainerRightP2.ResumeLayout(false);
            splitContainerCentre.Panel1.ResumeLayout(false);
            splitContainerCentre.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerCentre).EndInit();
            splitContainerCentre.ResumeLayout(false);
            tableLayoutPanelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerLeftP1;
        private SplitContainer splitContainerRightP2;
        private SplitContainer splitContainerCentre;
        private Button buttonTestBinanceOutput;
        private Button buttonTestOutput;
        private Button buttonForm2;
        private Button buttonForm3;
        private TableLayoutPanel tableLayoutPanelRight;
        private UserControls.Output.OutputUserControl outputUserControl1;
        private Button buttonError;
        private Button buttonTestOutputWindow;
        private TableLayoutPanel tableLayoutPanelLeft;
        private TableLayoutPanel tableLayoutPanelExchange;
        private Label labelExchange;
        private ComboBox comboBoxExchange;
        private TabControl tabControlApiThrottle;
        private UserControls.Collections.MarketListCollectionUserControl marketListUserControl;
    }
}