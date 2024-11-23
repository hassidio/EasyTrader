namespace EasyTrader.Forms
{
    partial class Form3
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
            apiThrottleUserControl1 = new UserControls.ApiThrottle.ApiThrottleUserControl();
            button1 = new Button();
            SuspendLayout();
            // 
            // apiThrottleUserControl1
            // 
            apiThrottleUserControl1.Dock = DockStyle.Bottom;
            apiThrottleUserControl1.Location = new Point(0, 127);
            apiThrottleUserControl1.Name = "apiThrottleUserControl1";
            apiThrottleUserControl1.Size = new Size(745, 365);
            apiThrottleUserControl1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(130, 48);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 492);
            Controls.Add(button1);
            Controls.Add(apiThrottleUserControl1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserControls.ApiThrottle.ApiThrottleUserControl apiThrottleUserControl1;
        private Button button1;
    }
}