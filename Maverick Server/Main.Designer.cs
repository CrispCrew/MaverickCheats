namespace Main
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TCPExceptions = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.HTTPExceptions = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.TCPInstancesLabel = new System.Windows.Forms.Label();
            this.HTTPInstancesLabel = new System.Windows.Forms.Label();
            this.ProductInstancesLabel = new System.Windows.Forms.Label();
            this.AuthTokenInstancesLabel = new System.Windows.Forms.Label();
            this.OAuthInstancesLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(756, 388);
            this.textBox1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TCP Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTTP Logs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 6);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(756, 388);
            this.textBox2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.TCPExceptions);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 400);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "TCP Exceptions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // TCPExceptions
            // 
            this.TCPExceptions.Location = new System.Drawing.Point(6, 6);
            this.TCPExceptions.Multiline = true;
            this.TCPExceptions.Name = "TCPExceptions";
            this.TCPExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TCPExceptions.Size = new System.Drawing.Size(756, 388);
            this.TCPExceptions.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.HTTPExceptions);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 400);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "HTTP Exceptions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // HTTPExceptions
            // 
            this.HTTPExceptions.Location = new System.Drawing.Point(6, 6);
            this.HTTPExceptions.Multiline = true;
            this.HTTPExceptions.Name = "HTTPExceptions";
            this.HTTPExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HTTPExceptions.Size = new System.Drawing.Size(756, 388);
            this.HTTPExceptions.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.RefreshButton);
            this.tabPage5.Controls.Add(this.ProductInstancesLabel);
            this.tabPage5.Controls.Add(this.AuthTokenInstancesLabel);
            this.tabPage5.Controls.Add(this.OAuthInstancesLabel);
            this.tabPage5.Controls.Add(this.HTTPInstancesLabel);
            this.tabPage5.Controls.Add(this.TCPInstancesLabel);
            this.tabPage5.Controls.Add(this.VersionLabel);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(768, 400);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Cache Logs";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(6, 3);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(48, 13);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "Version: ";
            // 
            // TCPInstancesLabel
            // 
            this.TCPInstancesLabel.AutoSize = true;
            this.TCPInstancesLabel.Location = new System.Drawing.Point(6, 16);
            this.TCPInstancesLabel.Name = "TCPInstancesLabel";
            this.TCPInstancesLabel.Size = new System.Drawing.Size(83, 13);
            this.TCPInstancesLabel.TabIndex = 1;
            this.TCPInstancesLabel.Text = "TCP Instances: ";
            // 
            // HTTPInstancesLabel
            // 
            this.HTTPInstancesLabel.AutoSize = true;
            this.HTTPInstancesLabel.Location = new System.Drawing.Point(6, 29);
            this.HTTPInstancesLabel.Name = "HTTPInstancesLabel";
            this.HTTPInstancesLabel.Size = new System.Drawing.Size(91, 13);
            this.HTTPInstancesLabel.TabIndex = 2;
            this.HTTPInstancesLabel.Text = "HTTP Instances: ";
            // 
            // ProductInstancesLabel
            // 
            this.ProductInstancesLabel.AutoSize = true;
            this.ProductInstancesLabel.Location = new System.Drawing.Point(6, 68);
            this.ProductInstancesLabel.Name = "ProductInstancesLabel";
            this.ProductInstancesLabel.Size = new System.Drawing.Size(152, 13);
            this.ProductInstancesLabel.TabIndex = 5;
            this.ProductInstancesLabel.Text = "Number of Products in Cache: ";
            // 
            // AuthTokenInstancesLabel
            // 
            this.AuthTokenInstancesLabel.AutoSize = true;
            this.AuthTokenInstancesLabel.Location = new System.Drawing.Point(6, 55);
            this.AuthTokenInstancesLabel.Name = "AuthTokenInstancesLabel";
            this.AuthTokenInstancesLabel.Size = new System.Drawing.Size(115, 13);
            this.AuthTokenInstancesLabel.TabIndex = 4;
            this.AuthTokenInstancesLabel.Text = "AuthToken Instances: ";
            // 
            // OAuthInstancesLabel
            // 
            this.OAuthInstancesLabel.AutoSize = true;
            this.OAuthInstancesLabel.Location = new System.Drawing.Point(6, 42);
            this.OAuthInstancesLabel.Name = "OAuthInstancesLabel";
            this.OAuthInstancesLabel.Size = new System.Drawing.Size(92, 13);
            this.OAuthInstancesLabel.TabIndex = 3;
            this.OAuthInstancesLabel.Text = "OAuth Instances: ";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(687, 6);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 6;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox HTTPExceptions;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox TCPExceptions;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label ProductInstancesLabel;
        private System.Windows.Forms.Label AuthTokenInstancesLabel;
        private System.Windows.Forms.Label OAuthInstancesLabel;
        private System.Windows.Forms.Label HTTPInstancesLabel;
        private System.Windows.Forms.Label TCPInstancesLabel;
        private System.Windows.Forms.Button RefreshButton;
    }
}

