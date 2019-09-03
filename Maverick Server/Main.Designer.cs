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
            this.TCPLogs = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.RequestLogsButton = new System.Windows.Forms.Button();
            this.RequestLogs = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.RateLimitInstancesLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ProductInstancesLabel = new System.Windows.Forms.Label();
            this.AuthTokenInstancesLabel = new System.Windows.Forms.Label();
            this.OAuthInstancesLabel = new System.Windows.Forms.Label();
            this.HTTPInstancesLabel = new System.Windows.Forms.Label();
            this.TCPInstancesLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.CopyRateLimitLogs = new System.Windows.Forms.Button();
            this.RateLimitingLogs = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TCPLogsButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.HTTPLogs = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TCPExceptionsButton = new System.Windows.Forms.Button();
            this.TCPExceptions = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.HTTPExceptions = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // TCPLogs
            // 
            this.TCPLogs.Location = new System.Drawing.Point(6, 6);
            this.TCPLogs.MaxLength = 65534;
            this.TCPLogs.Multiline = true;
            this.TCPLogs.Name = "TCPLogs";
            this.TCPLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TCPLogs.Size = new System.Drawing.Size(756, 359);
            this.TCPLogs.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
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
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.RequestLogsButton);
            this.tabPage7.Controls.Add(this.RequestLogs);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(768, 400);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Request Logs";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // RequestLogsButton
            // 
            this.RequestLogsButton.Location = new System.Drawing.Point(687, 372);
            this.RequestLogsButton.Name = "RequestLogsButton";
            this.RequestLogsButton.Size = new System.Drawing.Size(75, 23);
            this.RequestLogsButton.TabIndex = 7;
            this.RequestLogsButton.Text = "Copy";
            this.RequestLogsButton.UseVisualStyleBackColor = true;
            this.RequestLogsButton.Click += new System.EventHandler(this.RequestLogsButton_Click);
            // 
            // RequestLogs
            // 
            this.RequestLogs.Location = new System.Drawing.Point(6, 6);
            this.RequestLogs.MaxLength = 65534;
            this.RequestLogs.Multiline = true;
            this.RequestLogs.Name = "RequestLogs";
            this.RequestLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RequestLogs.Size = new System.Drawing.Size(756, 360);
            this.RequestLogs.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.UpdateButton);
            this.tabPage5.Controls.Add(this.RateLimitInstancesLabel);
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
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(687, 371);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 7;
            this.UpdateButton.Text = "Shutdown";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // RateLimitInstancesLabel
            // 
            this.RateLimitInstancesLabel.AutoSize = true;
            this.RateLimitInstancesLabel.Location = new System.Drawing.Point(6, 68);
            this.RateLimitInstancesLabel.Name = "RateLimitInstancesLabel";
            this.RateLimitInstancesLabel.Size = new System.Drawing.Size(117, 13);
            this.RateLimitInstancesLabel.TabIndex = 7;
            this.RateLimitInstancesLabel.Text = "RateLimiting Instances:";
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
            // ProductInstancesLabel
            // 
            this.ProductInstancesLabel.AutoSize = true;
            this.ProductInstancesLabel.Location = new System.Drawing.Point(6, 81);
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
            // HTTPInstancesLabel
            // 
            this.HTTPInstancesLabel.AutoSize = true;
            this.HTTPInstancesLabel.Location = new System.Drawing.Point(6, 29);
            this.HTTPInstancesLabel.Name = "HTTPInstancesLabel";
            this.HTTPInstancesLabel.Size = new System.Drawing.Size(91, 13);
            this.HTTPInstancesLabel.TabIndex = 2;
            this.HTTPInstancesLabel.Text = "HTTP Instances: ";
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
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(6, 3);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(48, 13);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "Version: ";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.CopyRateLimitLogs);
            this.tabPage6.Controls.Add(this.RateLimitingLogs);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(768, 400);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "RateLimit Logs";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // CopyRateLimitLogs
            // 
            this.CopyRateLimitLogs.Location = new System.Drawing.Point(687, 371);
            this.CopyRateLimitLogs.Name = "CopyRateLimitLogs";
            this.CopyRateLimitLogs.Size = new System.Drawing.Size(75, 23);
            this.CopyRateLimitLogs.TabIndex = 8;
            this.CopyRateLimitLogs.Text = "Copy";
            this.CopyRateLimitLogs.UseVisualStyleBackColor = true;
            this.CopyRateLimitLogs.Click += new System.EventHandler(this.RateLimitLogs_Click);
            // 
            // RateLimitingLogs
            // 
            this.RateLimitingLogs.Location = new System.Drawing.Point(6, 6);
            this.RateLimitingLogs.MaxLength = 65534;
            this.RateLimitingLogs.Multiline = true;
            this.RateLimitingLogs.Name = "RateLimitingLogs";
            this.RateLimitingLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RateLimitingLogs.Size = new System.Drawing.Size(756, 359);
            this.RateLimitingLogs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TCPLogsButton);
            this.tabPage1.Controls.Add(this.TCPLogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TCP Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TCPLogsButton
            // 
            this.TCPLogsButton.Location = new System.Drawing.Point(687, 371);
            this.TCPLogsButton.Name = "TCPLogsButton";
            this.TCPLogsButton.Size = new System.Drawing.Size(75, 23);
            this.TCPLogsButton.TabIndex = 9;
            this.TCPLogsButton.Text = "Copy";
            this.TCPLogsButton.UseVisualStyleBackColor = true;
            this.TCPLogsButton.Click += new System.EventHandler(this.TCPLogsButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.HTTPLogs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTTP Logs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // HTTPLogs
            // 
            this.HTTPLogs.Location = new System.Drawing.Point(6, 6);
            this.HTTPLogs.MaxLength = 65534;
            this.HTTPLogs.Multiline = true;
            this.HTTPLogs.Name = "HTTPLogs";
            this.HTTPLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HTTPLogs.Size = new System.Drawing.Size(756, 388);
            this.HTTPLogs.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.TCPExceptionsButton);
            this.tabPage3.Controls.Add(this.TCPExceptions);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 400);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "TCP Exceptions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // TCPExceptionsButton
            // 
            this.TCPExceptionsButton.Location = new System.Drawing.Point(687, 371);
            this.TCPExceptionsButton.Name = "TCPExceptionsButton";
            this.TCPExceptionsButton.Size = new System.Drawing.Size(75, 23);
            this.TCPExceptionsButton.TabIndex = 10;
            this.TCPExceptionsButton.Text = "Copy";
            this.TCPExceptionsButton.UseVisualStyleBackColor = true;
            this.TCPExceptionsButton.Click += new System.EventHandler(this.TCPExceptionsButton_Click);
            // 
            // TCPExceptions
            // 
            this.TCPExceptions.Location = new System.Drawing.Point(6, 6);
            this.TCPExceptions.MaxLength = 65534;
            this.TCPExceptions.Multiline = true;
            this.TCPExceptions.Name = "TCPExceptions";
            this.TCPExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TCPExceptions.Size = new System.Drawing.Size(756, 359);
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
            this.HTTPExceptions.MaxLength = 65534;
            this.HTTPExceptions.Multiline = true;
            this.HTTPExceptions.Name = "HTTPExceptions";
            this.HTTPExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HTTPExceptions.Size = new System.Drawing.Size(756, 388);
            this.HTTPExceptions.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TCPLogs;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox HTTPLogs;
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
        private System.Windows.Forms.Label RateLimitInstancesLabel;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox RateLimitingLogs;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox RequestLogs;
        private System.Windows.Forms.Button RequestLogsButton;
        private System.Windows.Forms.Button CopyRateLimitLogs;
        private System.Windows.Forms.Button TCPLogsButton;
        private System.Windows.Forms.Button TCPExceptionsButton;
    }
}

