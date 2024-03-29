﻿using System;
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
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            BunifuAnimatorNS.Animation animation3 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation2 = new BunifuAnimatorNS.Animation();
            this.bunifuElipse1 = new ns1.BunifuElipse(this.components);
            this.sideBar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.memberUsername = new ns1.BunifuCustomLabel();
            this.memberAvatar = new ns1.BunifuImageButton();
            this.slidingPanel = new ns1.BunifuImageButton();
            this.topBar = new System.Windows.Forms.Panel();
            this.ShowLogs = new ns1.BunifuImageButton();
            this.loading = new System.Windows.Forms.PictureBox();
            this.Title = new ns1.BunifuCustomLabel();
            this.bunifuTransition1 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.launchButton = new ns1.BunifuThinButton2();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.requestFailed = new ns1.BunifuCustomLabel();
            this.BugReport = new ns1.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new ns1.BunifuCustomLabel();
            this.bunifuCustomLabel1 = new ns1.BunifuCustomLabel();
            this.bunifuDragControl1 = new ns1.BunifuDragControl(this.components);
            this.bunifuTransition2 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.bunifuTransition3 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.sideBar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memberAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidingPanel)).BeginInit();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.MainPanel.SuspendLayout();
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
            // memberUsername
            // 
            this.memberUsername.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTransition3.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.memberUsername, BunifuAnimatorNS.DecorationType.None);
            this.memberUsername.Font = new System.Drawing.Font("Calibri", 14F);
            this.memberUsername.ForeColor = System.Drawing.Color.White;
            this.memberUsername.Location = new System.Drawing.Point(69, 105);
            this.memberUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.memberUsername.Name = "memberUsername";
            this.memberUsername.Size = new System.Drawing.Size(148, 28);
            this.memberUsername.TabIndex = 7;
            this.memberUsername.Text = "Username";
            this.memberUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.memberAvatar.Click += new System.EventHandler(this.memberAvatar_Click);
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
            this.Title.Size = new System.Drawing.Size(257, 40);
            this.Title.TabIndex = 0;
            this.Title.Text = "MaverickCheats";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bunifuTransition1
            // 
            this.bunifuTransition1.AnimationType = BunifuAnimatorNS.AnimationType.Particles;
            this.bunifuTransition1.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 1;
            animation1.Padding = new System.Windows.Forms.Padding(100, 50, 100, 150);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 2F;
            animation1.TransparencyCoeff = 0F;
            this.bunifuTransition1.DefaultAnimation = animation1;
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
            this.launchButton.Location = new System.Drawing.Point(532, 471);
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
            this.MainPanel.Controls.Add(this.requestFailed);
            this.MainPanel.Controls.Add(this.BugReport);
            this.MainPanel.Controls.Add(this.bunifuCustomLabel2);
            this.MainPanel.Controls.Add(this.bunifuCustomLabel1);
            this.bunifuTransition1.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this.MainPanel, BunifuAnimatorNS.DecorationType.None);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(300, 40);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(657, 528);
            this.MainPanel.TabIndex = 7;
            // 
            // requestFailed
            // 
            this.requestFailed.BackColor = System.Drawing.Color.Red;
            this.bunifuTransition3.SetDecoration(this.requestFailed, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.requestFailed, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.requestFailed, BunifuAnimatorNS.DecorationType.None);
            this.requestFailed.Dock = System.Windows.Forms.DockStyle.Top;
            this.requestFailed.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestFailed.ForeColor = System.Drawing.Color.White;
            this.requestFailed.Location = new System.Drawing.Point(0, 0);
            this.requestFailed.Name = "requestFailed";
            this.requestFailed.Size = new System.Drawing.Size(657, 34);
            this.requestFailed.TabIndex = 20;
            this.requestFailed.Text = "Request Failed";
            this.requestFailed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.requestFailed.Visible = false;
            // 
            // BugReport
            // 
            this.BugReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition3.SetDecoration(this.BugReport, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.BugReport, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.BugReport, BunifuAnimatorNS.DecorationType.None);
            this.BugReport.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.BugReport.ForeColor = System.Drawing.Color.White;
            this.BugReport.Location = new System.Drawing.Point(292, 101);
            this.BugReport.Name = "BugReport";
            this.BugReport.Size = new System.Drawing.Size(97, 28);
            this.BugReport.TabIndex = 19;
            this.BugReport.Text = "Report Bugs";
            this.BugReport.Click += new System.EventHandler(this.BugReport_Click);
            this.BugReport.MouseEnter += new System.EventHandler(this.BugReport_MouseEnter);
            this.BugReport.MouseLeave += new System.EventHandler(this.BugReport_MouseLeave);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTransition3.SetDecoration(this.bunifuCustomLabel2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.bunifuCustomLabel2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.bunifuCustomLabel2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(1, 72);
            this.bunifuCustomLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(654, 29);
            this.bunifuCustomLabel2.TabIndex = 8;
            this.bunifuCustomLabel2.Text = "Click the Link below to send a Bug Report";
            this.bunifuCustomLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTransition3.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition2.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(1, 43);
            this.bunifuCustomLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(654, 29);
            this.bunifuCustomLabel1.TabIndex = 7;
            this.bunifuCustomLabel1.Text = "This is a BETA Launcher, Report Bugs to us so we can fix them.";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            animation3.AnimateOnlyDifferences = true;
            animation3.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.BlindCoeff")));
            animation3.LeafCoeff = 0F;
            animation3.MaxTime = 1F;
            animation3.MinTime = 0F;
            animation3.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicCoeff")));
            animation3.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicShift")));
            animation3.MosaicSize = 1;
            animation3.Padding = new System.Windows.Forms.Padding(100, 50, 100, 150);
            animation3.RotateCoeff = 0F;
            animation3.RotateLimit = 0F;
            animation3.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.ScaleCoeff")));
            animation3.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.SlideCoeff")));
            animation3.TimeCoeff = 2F;
            animation3.TransparencyCoeff = 0F;
            this.bunifuTransition2.DefaultAnimation = animation3;
            this.bunifuTransition2.Interval = 1;
            this.bunifuTransition2.MaxAnimationTime = 5000;
            this.bunifuTransition2.TimeStep = 0.01F;
            // 
            // bunifuTransition3
            // 
            this.bunifuTransition3.AnimationType = BunifuAnimatorNS.AnimationType.ScaleAndRotate;
            this.bunifuTransition3.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(30);
            animation2.RotateCoeff = 0.5F;
            animation2.RotateLimit = 0.2F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.bunifuTransition3.DefaultAnimation = animation2;
            this.bunifuTransition3.Interval = 1;
            this.bunifuTransition3.MaxAnimationTime = 5000;
            this.bunifuTransition3.TimeStep = 0.01F;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(957, 568);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.topBar);
            this.Controls.Add(this.sideBar);
            this.bunifuTransition2.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition1.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTransition3.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.sideBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memberAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidingPanel)).EndInit();
            this.topBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShowLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.MainPanel.ResumeLayout(false);
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
        private ns1.BunifuCustomLabel bunifuCustomLabel2;
        private ns1.BunifuCustomLabel bunifuCustomLabel1;
        private ns1.BunifuCustomLabel BugReport;
        private ns1.BunifuCustomLabel requestFailed;
    }
}

