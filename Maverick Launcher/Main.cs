using Main.AuthLib;
using NetworkTypes;
using ns1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Main
{
    public partial class Main : Form
    {
        /// <summary>
        /// Connection to the Server
        /// </summary>
        public Client client;

        /// <summary>
        /// Authentication Token
        /// </summary>
        public Token token;

        public Main(Client client, Token token)
        {
            this.client = client;
            this.token = token;

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (panel1.Width == 50)
            {
                panel1.Visible = false;
                panel1.Width = 260;

                bunifuTransition1.ShowSync(panel1);
            }
            else
            {
                panel1.Visible = false;
                panel1.Width = 50;

                bunifuTransition1.ShowSync(panel1);
            }

            foreach (Product product in client.Products(token))
            {
                Console.WriteLine(product.Id + ", " + product.Name + "," + product.Image.LongLength);

                BunifuFlatButton CheatListTab = new BunifuFlatButton();
                CheatListTab.Tag = product.Id;

                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));

                // 
                // bunifuFlatButton1
                // 
                CheatListTab.Activecolor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
                CheatListTab.BackColor = Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
                CheatListTab.BackgroundImageLayout = ImageLayout.Stretch;
                CheatListTab.BorderRadius = 0;
                CheatListTab.Cursor = System.Windows.Forms.Cursors.Hand;
                bunifuTransition1.SetDecoration(CheatListTab, BunifuAnimatorNS.DecorationType.None);
                CheatListTab.DisabledColor = Color.Gray;
                CheatListTab.Iconcolor = Color.Transparent;
                CheatListTab.Iconimage = Image.FromStream(new MemoryStream(product.Image));
                CheatListTab.Iconimage_right = null;
                CheatListTab.Iconimage_right_Selected = null;
                CheatListTab.Iconimage_Selected = null;
                CheatListTab.IconMarginLeft = 0;
                CheatListTab.IconMarginRight = 0;
                CheatListTab.IconRightVisible = true;
                CheatListTab.IconRightZoom = 0D;
                CheatListTab.IconVisible = true;
                CheatListTab.IconZoom = 65;
                CheatListTab.IsTab = true;
                CheatListTab.Name = "button";
                CheatListTab.Normalcolor = Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
                CheatListTab.OnHovercolor = Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
                CheatListTab.OnHoverTextColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
                CheatListTab.Size = new Size(303, 48);
                CheatListTab.TabIndex = product.Id;
                CheatListTab.Text = "  " + product.Name;
                CheatListTab.TextAlign = ContentAlignment.MiddleLeft;
                CheatListTab.Textcolor = Color.Silver;
                CheatListTab.TextFont = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                CheatListTab.Click += new EventHandler(CheatListTab_Click);
                CheatListTab.MouseEnter += new EventHandler(CheatListTabs_Hover);
                CheatListTab.MouseLeave += new EventHandler(CheatListTabs_Leave);

                flowLayoutPanel1.Controls.Add(CheatListTab);
            }
        }

        #region Event Handlers - Front End
        /// <summary>
        /// Creates a new Windows Form with a TextBox Log - Optional Button that can Upload Logs to the Server for Review
        /// Maybe integrate with Support Tickets to auto create a ticket with the Log
        private void ShowLogsButton_Click(object sender, EventArgs e)
        {
            //Create Window with Logs
        }

        #region Sliding Panel Events
        private void slidingPanel_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 50)
            {
                panel1.Visible = false;
                panel1.Width = 260;

                bunifuTransition1.ShowSync(panel1);
            }
            else
            {
                panel1.Visible = false;
                panel1.Width = 50;

                bunifuTransition1.ShowSync(panel1);
            }
        }
        #endregion

        #region CheatList Events
        //Check if other tabs are clicked, deselect them if they are
        private void CheatListTab_Click(object sender, EventArgs e)
        {
            BunifuFlatButton button = ((BunifuFlatButton)sender);

            foreach (BunifuFlatButton control in flowLayoutPanel1.Controls)
                if (control.selected && button != control)
                    control.selected = false;
        }

        private void CheatListTabs_Hover(object sender, EventArgs e)
        {
            ((BunifuFlatButton)sender).IconZoom = ((BunifuFlatButton)sender).IconZoom + 10;
        }

        private void CheatListTabs_Leave(object sender, EventArgs e)
        {
            ((BunifuFlatButton)sender).IconZoom = ((BunifuFlatButton)sender).IconZoom - 10;
        }
        #endregion
        #endregion
    }
}
