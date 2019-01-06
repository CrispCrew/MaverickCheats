using System.Drawing;
using System.IO;
using System.Linq;

namespace Main
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuImageButton4 = new ns1.BunifuImageButton();
            this.bunifuImageButton2 = new ns1.BunifuImageButton();
            this.ShowLogsButton = new ns1.BunifuImageButton();
            this.bunifuImageButton1 = new ns1.BunifuImageButton();
            this.Username = new ns1.BunifuMaterialTextbox();
            this.Password = new ns1.BunifuMaterialTextbox();
            this.bunifuCustomLabel1 = new ns1.BunifuCustomLabel();
            this.rememberMeCheckBox = new ns1.BunifuCheckbox();
            this.loginButton = new ns1.BunifuThinButton2();
            this.bunifuCustomLabel2 = new ns1.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new ns1.BunifuCustomLabel();
            this.autoLoginCheckBox = new ns1.BunifuCheckbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bunifuImageButton5 = new ns1.BunifuImageButton();
            this.bunifuImageButton3 = new ns1.BunifuImageButton();
            this.bunifuCustomLabel4 = new ns1.BunifuCustomLabel();
            this.failedLogin = new ns1.BunifuCustomLabel();
            this.loginForum = new ns1.BunifuCustomLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.bunifuImageButton4);
            this.panel2.Controls.Add(this.bunifuImageButton2);
            this.panel2.Controls.Add(this.ShowLogsButton);
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 400);
            this.panel2.TabIndex = 2;
            // 
            // bunifuImageButton4
            // 
            this.bunifuImageButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bunifuImageButton4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton4.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton4.Image")));
            this.bunifuImageButton4.ImageActive = null;
            this.bunifuImageButton4.Location = new System.Drawing.Point(142, 163);
            this.bunifuImageButton4.Name = "bunifuImageButton4";
            this.bunifuImageButton4.Size = new System.Drawing.Size(75, 75);
            this.bunifuImageButton4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton4.TabIndex = 10;
            this.bunifuImageButton4.TabStop = false;
            this.bunifuImageButton4.Zoom = 10;
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.bunifuImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(-375, 5);
            this.bunifuImageButton2.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(31, 30);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 8;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            // 
            // ShowLogsButton
            // 
            this.ShowLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowLogsButton.BackColor = System.Drawing.Color.Transparent;
            this.ShowLogsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowLogsButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowLogsButton.Image")));
            this.ShowLogsButton.ImageActive = null;
            this.ShowLogsButton.Location = new System.Drawing.Point(-587, 6);
            this.ShowLogsButton.Margin = new System.Windows.Forms.Padding(2);
            this.ShowLogsButton.Name = "ShowLogsButton";
            this.ShowLogsButton.Size = new System.Drawing.Size(10, 30);
            this.ShowLogsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShowLogsButton.TabIndex = 6;
            this.ShowLogsButton.TabStop = false;
            this.ShowLogsButton.Zoom = 10;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(-43, 6);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(31, 30);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 7;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            // 
            // Username
            // 
            this.Username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.Username.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Username.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.Username.ForeColor = System.Drawing.Color.Silver;
            this.Username.HintForeColor = System.Drawing.Color.Empty;
            this.Username.HintText = "";
            this.Username.isPassword = false;
            this.Username.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(22)))), ((int)(((byte)(185)))));
            this.Username.LineIdleColor = System.Drawing.Color.Gray;
            this.Username.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(104)))), ((int)(((byte)(184)))));
            this.Username.LineThickness = 2;
            this.Username.Location = new System.Drawing.Point(409, 158);
            this.Username.Margin = new System.Windows.Forms.Padding(4);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(286, 37);
            this.Username.TabIndex = 1;
            this.Username.Text = "Username";
            this.Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Username.Enter += new System.EventHandler(this.Username_Enter);
            this.Username.Leave += new System.EventHandler(this.Username_Leave);
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Password.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.Password.ForeColor = System.Drawing.Color.Silver;
            this.Password.HintForeColor = System.Drawing.Color.Empty;
            this.Password.HintText = "";
            this.Password.isPassword = false;
            this.Password.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(22)))), ((int)(((byte)(185)))));
            this.Password.LineIdleColor = System.Drawing.Color.Gray;
            this.Password.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(104)))), ((int)(((byte)(184)))));
            this.Password.LineThickness = 2;
            this.Password.Location = new System.Drawing.Point(409, 239);
            this.Password.Margin = new System.Windows.Forms.Padding(4);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(286, 37);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Password.Enter += new System.EventHandler(this.Password_Enter);
            this.Password.Leave += new System.EventHandler(this.Password_Leave);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.Silver;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(398, 40);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(162, 58);
            this.bunifuCustomLabel1.TabIndex = 10;
            this.bunifuCustomLabel1.Text = "Login";
            // 
            // rememberMeCheckBox
            // 
            this.rememberMeCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.rememberMeCheckBox.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.rememberMeCheckBox.Checked = false;
            this.rememberMeCheckBox.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.rememberMeCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rememberMeCheckBox.ForeColor = System.Drawing.Color.White;
            this.rememberMeCheckBox.Location = new System.Drawing.Point(409, 312);
            this.rememberMeCheckBox.Name = "rememberMeCheckBox";
            this.rememberMeCheckBox.Size = new System.Drawing.Size(20, 20);
            this.rememberMeCheckBox.TabIndex = 3;
            // 
            // loginButton
            // 
            this.loginButton.ActiveBorderThickness = 1;
            this.loginButton.ActiveCornerRadius = 30;
            this.loginButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.loginButton.ActiveForecolor = System.Drawing.Color.White;
            this.loginButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.loginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.loginButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginButton.BackgroundImage")));
            this.loginButton.ButtonText = "Login";
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.ForeColor = System.Drawing.Color.White;
            this.loginButton.IdleBorderThickness = 1;
            this.loginButton.IdleCornerRadius = 30;
            this.loginButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.loginButton.IdleForecolor = System.Drawing.Color.White;
            this.loginButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.loginButton.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.loginButton.Location = new System.Drawing.Point(581, 349);
            this.loginButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(112, 52);
            this.loginButton.TabIndex = 5;
            this.loginButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.Silver;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(432, 314);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(90, 15);
            this.bunifuCustomLabel2.TabIndex = 13;
            this.bunifuCustomLabel2.Text = "Remember Me";
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.Silver;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(432, 365);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(66, 15);
            this.bunifuCustomLabel3.TabIndex = 15;
            this.bunifuCustomLabel3.Text = "Auto Login";
            // 
            // autoLoginCheckBox
            // 
            this.autoLoginCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.autoLoginCheckBox.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.autoLoginCheckBox.Checked = false;
            this.autoLoginCheckBox.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.autoLoginCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoLoginCheckBox.ForeColor = System.Drawing.Color.White;
            this.autoLoginCheckBox.Location = new System.Drawing.Point(409, 363);
            this.autoLoginCheckBox.Name = "autoLoginCheckBox";
            this.autoLoginCheckBox.Size = new System.Drawing.Size(20, 20);
            this.autoLoginCheckBox.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.bunifuImageButton5);
            this.panel1.Controls.Add(this.bunifuImageButton3);
            this.panel1.Controls.Add(this.bunifuCustomLabel4);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 40);
            this.panel1.TabIndex = 16;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(706, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // bunifuImageButton5
            // 
            this.bunifuImageButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton5.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton5.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton5.Image")));
            this.bunifuImageButton5.ImageActive = null;
            this.bunifuImageButton5.Location = new System.Drawing.Point(12, 6);
            this.bunifuImageButton5.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton5.Name = "bunifuImageButton5";
            this.bunifuImageButton5.Size = new System.Drawing.Size(31, 30);
            this.bunifuImageButton5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton5.TabIndex = 17;
            this.bunifuImageButton5.TabStop = false;
            this.bunifuImageButton5.Zoom = 10;
            // 
            // bunifuImageButton3
            // 
            this.bunifuImageButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton3.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton3.Image")));
            this.bunifuImageButton3.ImageActive = null;
            this.bunifuImageButton3.Location = new System.Drawing.Point(-201, 6);
            this.bunifuImageButton3.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton3.Name = "bunifuImageButton3";
            this.bunifuImageButton3.Size = new System.Drawing.Size(31, 30);
            this.bunifuImageButton3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton3.TabIndex = 6;
            this.bunifuImageButton3.TabStop = false;
            this.bunifuImageButton3.Zoom = 10;
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(46, 0);
            this.bunifuCustomLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(133, 40);
            this.bunifuCustomLabel4.TabIndex = 0;
            this.bunifuCustomLabel4.Text = "MaverickCheats";
            this.bunifuCustomLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // failedLogin
            // 
            this.failedLogin.BackColor = System.Drawing.Color.Red;
            this.failedLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedLogin.ForeColor = System.Drawing.Color.White;
            this.failedLogin.Location = new System.Drawing.Point(360, 406);
            this.failedLogin.Name = "failedLogin";
            this.failedLogin.Size = new System.Drawing.Size(384, 34);
            this.failedLogin.TabIndex = 17;
            this.failedLogin.Text = "Login Failed";
            this.failedLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.failedLogin.Visible = false;
            // 
            // loginForum
            // 
            this.loginForum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginForum.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.loginForum.ForeColor = System.Drawing.Color.White;
            this.loginForum.Location = new System.Drawing.Point(568, 312);
            this.loginForum.Name = "loginForum";
            this.loginForum.Size = new System.Drawing.Size(139, 28);
            this.loginForum.TabIndex = 18;
            this.loginForum.Text = "Login with Website";
            this.loginForum.Click += new System.EventHandler(this.loginForum_Click);
            this.loginForum.MouseEnter += new System.EventHandler(this.loginForum_MouseEnter);
            this.loginForum.MouseLeave += new System.EventHandler(this.loginForum_MouseLeave);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(745, 440);
            this.Controls.Add(this.loginForum);
            this.Controls.Add(this.failedLogin);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.autoLoginCheckBox);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.rememberMeCheckBox);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.bunifuImageButton1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private ns1.BunifuImageButton ShowLogsButton;
        private ns1.BunifuImageButton bunifuImageButton1;
        private ns1.BunifuImageButton bunifuImageButton2;
        private ns1.BunifuImageButton bunifuImageButton4;
        private ns1.BunifuMaterialTextbox Username;
        private ns1.BunifuMaterialTextbox Password;
        private ns1.BunifuCustomLabel bunifuCustomLabel1;
        private ns1.BunifuCheckbox rememberMeCheckBox;
        private ns1.BunifuThinButton2 loginButton;
        private ns1.BunifuCustomLabel bunifuCustomLabel2;
        private ns1.BunifuCustomLabel bunifuCustomLabel3;
        private ns1.BunifuCheckbox autoLoginCheckBox;
        private System.Windows.Forms.Panel panel1;
        private ns1.BunifuImageButton bunifuImageButton3;
        private ns1.BunifuCustomLabel bunifuCustomLabel4;
        private ns1.BunifuImageButton bunifuImageButton5;
        private ns1.BunifuCustomLabel failedLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ns1.BunifuCustomLabel loginForum;
    }
}