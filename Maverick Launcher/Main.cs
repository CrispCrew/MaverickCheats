using Main.AuthLib;
using NetworkTypes;
using ns1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
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

        /// <summary>
        /// List of Owned Products
        /// </summary>
        public List<Product> products;

        public Main(Client client, Token token)
        {
            this.client = client;
            this.token = token;

            InitializeComponent();

            pictureBox1.Image = Image.FromStream(new MemoryStream(EmbeddedResource.EmbeddedResources.First(resource => resource.Key == "Spinner.gif").Value));
        }

        private void Main_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate { pictureBox1.Visible = true; });

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

                products = client.Products(token);

                foreach (Product product in products)
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
                    CheatListTab.OnHoverTextColor = Color.White;
                    CheatListTab.Size = new Size(303, 48);
                    CheatListTab.TabIndex = product.Id;
                    CheatListTab.Text = "  " + product.Name;
                    CheatListTab.TextAlign = ContentAlignment.MiddleLeft;
                    CheatListTab.Textcolor = Color.Silver;
                    CheatListTab.TextFont = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    CheatListTab.MouseDown += new EventHandler(CheatListTab_MouseDown);
                    CheatListTab.MouseEnter += new EventHandler(CheatListTabs_Hover);
                    CheatListTab.MouseLeave += new EventHandler(CheatListTabs_Leave);

                    this.BeginInvoke((MethodInvoker)delegate { flowLayoutPanel1.Controls.Add(CheatListTab); });
                }

                this.BeginInvoke((MethodInvoker)delegate { pictureBox1.Visible = false; });
            }).Start();
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
        private void CheatListTab_MouseDown(object sender, EventArgs e)
        {
            BunifuFlatButton button = ((BunifuFlatButton)sender);

            foreach (BunifuFlatButton control in flowLayoutPanel1.Controls)
                if (control.selected && button != control)
                    control.selected = false;

            if (((MouseEventArgs)e).Clicks < 2)
                return;

            new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate { pictureBox1.Visible = true; });

                int ProductID = (int)((BunifuFlatButton)sender).Tag;

                ZipArchive archive = new ZipArchive(client.Download(token, products.Find(product => product.Id == ProductID)), ZipArchiveMode.Read);
                archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + products.Find(product => product.Id == ProductID).Name + "\\");
                archive.Dispose();

                this.BeginInvoke((MethodInvoker)delegate { pictureBox1.Visible = false; });
            }).Start();
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
