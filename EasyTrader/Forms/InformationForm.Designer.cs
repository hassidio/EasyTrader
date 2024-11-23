namespace EasyTrader.Forms
{
    partial class InformationForm
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
            propertyDataUserControl1 = new UserControls.PropertyDataUserControl();
            SuspendLayout();
            // 
            // propertyDataUserControl1
            // 
            propertyDataUserControl1.AutoScroll = true;
            propertyDataUserControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyDataUserControl1.BackColor = Color.Transparent;
            propertyDataUserControl1.Dock = DockStyle.Fill;
            propertyDataUserControl1.LazyLoad = true;
            propertyDataUserControl1.Location = new Point(0, 0);
            propertyDataUserControl1.Margin = new Padding(0);
            propertyDataUserControl1.Name = "propertyDataUserControl1";
            propertyDataUserControl1.NumberOfPageItems = 10;
            propertyDataUserControl1.ParentControl = null;
            propertyDataUserControl1.PropertyDataObject = null;
            propertyDataUserControl1.Size = new Size(904, 685);
            propertyDataUserControl1.TabIndex = 0;
            // 
            // InformationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(904, 685);
            Controls.Add(propertyDataUserControl1);
            Name = "InformationForm";
            Text = "InformationForm";
            Load += InformationForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserControls.PropertyDataUserControl propertyDataUserControl1;
    }
}