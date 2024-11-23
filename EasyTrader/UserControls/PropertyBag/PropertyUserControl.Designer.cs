namespace EasyTrader.UserControls.PropertyBag
{
    partial class PropertyUserControl
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
            labelPropertyName = new Label();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.Transparent;
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(labelPropertyName, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(300, 25);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // labelPropertyName
            // 
            labelPropertyName.AutoSize = true;
            labelPropertyName.BackColor = Color.Transparent;
            labelPropertyName.Dock = DockStyle.Left;
            labelPropertyName.Font = new Font("Segoe UI", 8F);
            labelPropertyName.Location = new Point(0, 0);
            labelPropertyName.Margin = new Padding(0);
            labelPropertyName.Name = "labelPropertyName";
            labelPropertyName.Padding = new Padding(3, 2, 3, 0);
            labelPropertyName.Size = new Size(68, 25);
            labelPropertyName.TabIndex = 1;
            labelPropertyName.Text = "Property";
            labelPropertyName.TextAlign = ContentAlignment.TopRight;
            // 
            // PropertyUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            Name = "PropertyUserControl";
            Size = new Size(300, 25);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private Label labelPropertyName;
    }
}
