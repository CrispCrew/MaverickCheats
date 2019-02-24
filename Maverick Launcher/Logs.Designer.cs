namespace Main
{
    partial class Logs
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logs));
            this.bunifuElipse1 = new ns1.BunifuElipse(this.components);
            this.LogsTextBox = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.topBar = new System.Windows.Forms.Panel();
            this.ShowLogs = new ns1.BunifuImageButton();
            this.Title = new ns1.BunifuCustomLabel();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // LogsTextBox
            // 
            this.LogsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.LogsTextBox.BorderColor = System.Drawing.Color.DarkGray;
            this.LogsTextBox.ForeColor = System.Drawing.Color.White;
            this.LogsTextBox.Location = new System.Drawing.Point(12, 46);
            this.LogsTextBox.Multiline = true;
            this.LogsTextBox.Name = "LogsTextBox";
            this.LogsTextBox.ReadOnly = true;
            this.LogsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogsTextBox.Size = new System.Drawing.Size(776, 392);
            this.LogsTextBox.TabIndex = 2;
            this.LogsTextBox.Text = "Logs";
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.topBar.Controls.Add(this.ShowLogs);
            this.topBar.Controls.Add(this.Title);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Location = new System.Drawing.Point(0, 0);
            this.topBar.Margin = new System.Windows.Forms.Padding(2);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(800, 40);
            this.topBar.TabIndex = 3;
            // 
            // ShowLogs
            // 
            this.ShowLogs.BackColor = System.Drawing.Color.Transparent;
            this.ShowLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowLogs.Image = ((System.Drawing.Image)(resources.GetObject("ShowLogs.Image")));
            this.ShowLogs.ImageActive = null;
            this.ShowLogs.Location = new System.Drawing.Point(757, 2);
            this.ShowLogs.Margin = new System.Windows.Forms.Padding(2);
            this.ShowLogs.Name = "ShowLogs";
            this.ShowLogs.Size = new System.Drawing.Size(31, 30);
            this.ShowLogs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShowLogs.TabIndex = 23;
            this.ShowLogs.TabStop = false;
            this.ShowLogs.Zoom = 10;
            this.ShowLogs.Click += new System.EventHandler(this.ShowLogs_Click);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.Title.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(2, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(247, 40);
            this.Title.TabIndex = 0;
            this.Title.Text = "MaverickCheats - Logs";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.topBar);
            this.Controls.Add(this.LogsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Logs";
            this.Text = "Logs";
            this.Load += new System.EventHandler(this.Logs_Load);
            this.topBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ns1.BunifuElipse bunifuElipse1;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox LogsTextBox;
        private System.Windows.Forms.Panel topBar;
        private ns1.BunifuCustomLabel Title;
        private ns1.BunifuImageButton ShowLogs;
    }
}