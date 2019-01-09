using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            BunifuAnimatorNS.Animation animation9 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation8 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation7 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.bunifuElipse1 = new ns1.BunifuElipse(this.components);
            this.sideBar = new System.Windows.Forms.Panel();
            this.memberUsername = new ns1.BunifuCustomLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.memberAvatar = new ns1.BunifuImageButton();
            this.slidingPanel = new ns1.BunifuImageButton();
            this.topBar = new System.Windows.Forms.Panel();
            this.ShowLogs = new ns1.BunifuImageButton();
            this.loading = new System.Windows.Forms.PictureBox();
            this.Title = new ns1.BunifuCustomLabel();
            this.bunifuTransition1 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.launchButton = new ns1.BunifuThinButton2();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.bunifuDragControl1 = new ns1.BunifuDragControl(this.components);
            this.bunifuTransition2 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.bunifuTransition3 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.sideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memberAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidingPanel)).BeginInit();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.sideBar.Controls.Add(this.panel1);
            this.sideBar.Controls.Add(this.memberUsername);
            this.sideBar.Controls.Add(this.memberAvatar);
            this.sideBar.Controls.Add(this.slidingPanel);
            this.bunifuTransition1.SetDecoration(this.sideBar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.sideBar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.sideBar, BunifuAnimatorNS.DecorationType.None);
            this.sideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar.Location = new System.Drawing.Point(0, 0);
            this.sideBar.Margin = new System.Windows.Forms.Padding(2);
            this.sideBar.Name = "sideBar";
            this.sideBar.Size = new System.Drawing.Size(300, 568);
            this.sideBar.TabIndex = 0;
            // 
            // memberUsername
            // 
            this.memberUsername.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTransition3.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.memberUsername.Font = new System.Drawing.Font("Calibri", 14F);
            this.memberUsername.ForeColor = System.Drawing.Color.White;
            this.memberUsername.Location = new System.Drawing.Point(98, 105);
            this.memberUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.memberUsername.Name = "memberUsername";
            this.memberUsername.Size = new System.Drawing.Size(96, 28);
            this.memberUsername.TabIndex = 7;
            this.memberUsername.Text = "Username";
            this.memberUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.bunifuTransition3.SetDecoration(this.flowLayoutPanel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.flowLayoutPanel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.flowLayoutPanel1, BunifuAnimatorNS.DecorationType.None);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 432);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // memberAvatar
            // 
            this.memberAvatar.BackColor = System.Drawing.Color.Transparent;
            this.memberAvatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.memberAvatar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.memberAvatar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.memberAvatar, BunifuAnimatorNS.DecorationType.None);
            this.memberAvatar.Image = ((System.Drawing.Image)(resources.GetObject("memberAvatar.Image")));
            this.memberAvatar.ImageActive = null;
            this.memberAvatar.Location = new System.Drawing.Point(96, 12);
            this.memberAvatar.Name = "memberAvatar";
            this.memberAvatar.Size = new System.Drawing.Size(90, 90);
            this.memberAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.memberAvatar.TabIndex = 8;
            this.memberAvatar.TabStop = false;
            this.memberAvatar.Zoom = 10;
            // 
            // slidingPanel
            // 
            this.slidingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.slidingPanel.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTransition1.SetDecoration(this.slidingPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.slidingPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.slidingPanel, BunifuAnimatorNS.DecorationType.None);
            this.slidingPanel.Image = ((System.Drawing.Image)(resources.GetObject("slidingPanel.Image")));
            this.slidingPanel.ImageActive = null;
            this.slidingPanel.Location = new System.Drawing.Point(263, 7);
            this.slidingPanel.Margin = new System.Windows.Forms.Padding(2);
            this.slidingPanel.Name = "slidingPanel";
            this.slidingPanel.Size = new System.Drawing.Size(27, 26);
            this.slidingPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.slidingPanel.TabIndex = 0;
            this.slidingPanel.TabStop = false;
            this.slidingPanel.Zoom = 10;
            this.slidingPanel.Click += new System.EventHandler(this.slidingPanel_Click);
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.topBar.Controls.Add(this.ShowLogs);
            this.topBar.Controls.Add(this.loading);
            this.topBar.Controls.Add(this.Title);
            this.bunifuTransition1.SetDecoration(this.topBar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.topBar, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.topBar, BunifuAnimatorNS.DecorationType.None);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Location = new System.Drawing.Point(300, 0);
            this.topBar.Margin = new System.Windows.Forms.Padding(2);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(657, 40);
            this.topBar.TabIndex = 1;
            this.topBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topBar_MouseDown);
            this.topBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topBar_MouseMove);
            // 
            // ShowLogs
            // 
            this.ShowLogs.BackColor = System.Drawing.Color.Transparent;
            this.ShowLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.ShowLogs, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.ShowLogs, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.ShowLogs, BunifuAnimatorNS.DecorationType.None);
            this.ShowLogs.Image = ((System.Drawing.Image)(resources.GetObject("ShowLogs.Image")));
            this.ShowLogs.ImageActive = null;
            this.ShowLogs.Location = new System.Drawing.Point(5, 5);
            this.ShowLogs.Margin = new System.Windows.Forms.Padding(2);
            this.ShowLogs.Name = "ShowLogs";
            this.ShowLogs.Size = new System.Drawing.Size(31, 30);
            this.ShowLogs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShowLogs.TabIndex = 22;
            this.ShowLogs.TabStop = false;
            this.ShowLogs.Zoom = 10;
            this.ShowLogs.Click += new System.EventHandler(this.ShowLogs_Click);
            // 
            // loading
            // 
            this.loading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuTransition3.SetDecoration(this.loading, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.loading, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.loading, BunifuAnimatorNS.DecorationType.None);
            this.loading.Location = new System.Drawing.Point(617, 0);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(40, 40);
            this.loading.TabIndex = 21;
            this.loading.TabStop = false;
            this.loading.Visible = false;
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.bunifuTransition3.SetDecoration(this.Title, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.Title, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.Title, BunifuAnimatorNS.DecorationType.None);
            this.Title.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(40, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(182, 40);
            this.Title.TabIndex = 0;
            this.Title.Text = "MaverickCheats";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bunifuTransition1
            // 
            this.bunifuTransition1.AnimationType = BunifuAnimatorNS.AnimationType.Particles;
            this.bunifuTransition1.Cursor = null;
            animation9.AnimateOnlyDifferences = true;
            animation9.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation9.BlindCoeff")));
            animation9.LeafCoeff = 0F;
            animation9.MaxTime = 1F;
            animation9.MinTime = 0F;
            animation9.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation9.MosaicCoeff")));
            animation9.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation9.MosaicShift")));
            animation9.MosaicSize = 1;
            animation9.Padding = new System.Windows.Forms.Padding(100, 50, 100, 150);
            animation9.RotateCoeff = 0F;
            animation9.RotateLimit = 0F;
            animation9.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation9.ScaleCoeff")));
            animation9.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation9.SlideCoeff")));
            animation9.TimeCoeff = 2F;
            animation9.TransparencyCoeff = 0F;
            this.bunifuTransition1.DefaultAnimation = animation9;
            this.bunifuTransition1.Interval = 1;
            this.bunifuTransition1.MaxAnimationTime = 5000;
            this.bunifuTransition1.TimeStep = 0.01F;
            // 
            // launchButton
            // 
            this.launchButton.ActiveBorderThickness = 1;
            this.launchButton.ActiveCornerRadius = 30;
            this.launchButton.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.launchButton.ActiveForecolor = System.Drawing.Color.White;
            this.launchButton.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.launchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.launchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.launchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("launchButton.BackgroundImage")));
            this.launchButton.ButtonText = "Launch";
            this.launchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.launchButton, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.launchButton, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.launchButton, BunifuAnimatorNS.DecorationType.None);
            this.launchButton.Font = new System.Drawing.Font("Corbel", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchButton.ForeColor = System.Drawing.Color.White;
            this.launchButton.IdleBorderThickness = 1;
            this.launchButton.IdleCornerRadius = 30;
            this.launchButton.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.launchButton.IdleForecolor = System.Drawing.Color.White;
            this.launchButton.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.launchButton.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.launchButton.Location = new System.Drawing.Point(532, 502);
            this.launchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(112, 52);
            this.launchButton.TabIndex = 6;
            this.launchButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.launchButton);
            this.bunifuTransition1.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(300, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(657, 568);
            this.MainPanel.TabIndex = 7;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.topBar;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuTransition2
            // 
            this.bunifuTransition2.AnimationType = BunifuAnimatorNS.AnimationType.Particles;
            this.bunifuTransition2.Cursor = null;
            animation8.AnimateOnlyDifferences = true;
            animation8.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.BlindCoeff")));
            animation8.LeafCoeff = 0F;
            animation8.MaxTime = 1F;
            animation8.MinTime = 0F;
            animation8.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.MosaicCoeff")));
            animation8.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation8.MosaicShift")));
            animation8.MosaicSize = 1;
            animation8.Padding = new System.Windows.Forms.Padding(100, 50, 100, 150);
            animation8.RotateCoeff = 0F;
            animation8.RotateLimit = 0F;
            animation8.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.ScaleCoeff")));
            animation8.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.SlideCoeff")));
            animation8.TimeCoeff = 2F;
            animation8.TransparencyCoeff = 0F;
            this.bunifuTransition2.DefaultAnimation = animation8;
            this.bunifuTransition2.Interval = 1;
            this.bunifuTransition2.MaxAnimationTime = 5000;
            this.bunifuTransition2.TimeStep = 0.01F;
            // 
            // bunifuTransition3
            // 
            this.bunifuTransition3.AnimationType = BunifuAnimatorNS.AnimationType.ScaleAndRotate;
            this.bunifuTransition3.Cursor = null;
            animation7.AnimateOnlyDifferences = true;
            animation7.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.BlindCoeff")));
            animation7.LeafCoeff = 0F;
            animation7.MaxTime = 1F;
            animation7.MinTime = 0F;
            animation7.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.MosaicCoeff")));
            animation7.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation7.MosaicShift")));
            animation7.MosaicSize = 0;
            animation7.Padding = new System.Windows.Forms.Padding(30);
            animation7.RotateCoeff = 0.5F;
            animation7.RotateLimit = 0.2F;
            animation7.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.ScaleCoeff")));
            animation7.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.SlideCoeff")));
            animation7.TimeCoeff = 0F;
            animation7.TransparencyCoeff = 0F;
            this.bunifuTransition3.DefaultAnimation = animation7;
            this.bunifuTransition3.Interval = 1;
            this.bunifuTransition3.MaxAnimationTime = 5000;
            this.bunifuTransition3.TimeStep = 0.01F;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.bunifuTransition1.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Location = new System.Drawing.Point(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 415);
            this.panel1.TabIndex = 9;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(957, 568);
            this.Controls.Add(this.topBar);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.sideBar);
            this.bunifuTransition2.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Main_Load);
            this.sideBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memberAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidingPanel)).EndInit();
            this.topBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ns1.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Panel sideBar;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition1;
        private ns1.BunifuCustomLabel Title;
        private ns1.BunifuImageButton slidingPanel;
        private ns1.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ns1.BunifuThinButton2 launchButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox loading;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition2;
        private ns1.BunifuImageButton ShowLogs;
        private ns1.BunifuCustomLabel memberUsername;
        private ns1.BunifuImageButton memberAvatar;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition3;
        private Panel panel1;
    }
}

