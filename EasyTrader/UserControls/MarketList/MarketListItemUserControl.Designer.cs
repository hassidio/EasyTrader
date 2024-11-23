namespace EasyTrader.UserControls.MarketList
{
    partial class MarketListItemUserControl
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
            openPropertyButton1 = new ActionButtons.OpenPropertyButton();
            updateActionUserControl1 = new ActionButtons.ActionUserControl();
            labelName = new Label();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.Transparent;
            tableLayoutPanelMain.ColumnCount = 3;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
            tableLayoutPanelMain.Controls.Add(openPropertyButton1, 2, 0);
            tableLayoutPanelMain.Controls.Add(updateActionUserControl1, 0, 0);
            tableLayoutPanelMain.Controls.Add(labelName, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelMain.Size = new Size(413, 28);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // openPropertyButton1
            // 
            openPropertyButton1.Dock = DockStyle.Top;
            openPropertyButton1.Location = new Point(386, 0);
            openPropertyButton1.Margin = new Padding(0);
            openPropertyButton1.Name = "openPropertyButton1";
            openPropertyButton1.Size = new Size(27, 28);
            openPropertyButton1.TabIndex = 0;
            // 
            // updateActionUserControl1
            // 
            updateActionUserControl1.Action = null;
            updateActionUserControl1.BackColor = Color.Transparent;
            updateActionUserControl1.Dock = DockStyle.Top;
            updateActionUserControl1.ImageOff = Properties.Resources.Update_40;
            updateActionUserControl1.ImageOn = Properties.Resources.Update_on;
            updateActionUserControl1.ImagePanel = null;
            updateActionUserControl1.Location = new Point(1, 1);
            updateActionUserControl1.Margin = new Padding(1);
            updateActionUserControl1.Name = "updateActionUserControl1";
            updateActionUserControl1.Size = new Size(25, 26);
            updateActionUserControl1.TabIndex = 1;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.BackColor = Color.Transparent;
            labelName.Dock = DockStyle.Top;
            labelName.Location = new Point(32, 3);
            labelName.Margin = new Padding(5, 3, 3, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(351, 20);
            labelName.TabIndex = 2;
            labelName.Text = "labelName";
            labelName.Click += labelName_Click;
            labelName.MouseEnter += labelName_MouseEnter;
            labelName.MouseLeave += labelName_MouseLeave;
            // 
            // MarketListItemUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanelMain);
            Name = "MarketListItemUserControl";
            Size = new Size(413, 28);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private ActionButtons.OpenPropertyButton openPropertyButton1;
        private ActionButtons.ActionUserControl updateActionUserControl1;
        private Label labelName;
    }
}
