using ns1;
using Main.AuthLib;
using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            loading.Image = Image.FromStream(new MemoryStream(EmbeddedResource.EmbeddedResources.First(resource => resource.Key == "Spinner.gif").Value));

            sideBar.Width = 50;

            bunifuTransition2.Show(memberAvatar);

            memberAvatar.Image = token.Member.Avatar;
            memberUsername.Text = token.Member.Username;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

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
                    CheatListTab.Click += new EventHandler(CheatListTab_Click);
                    CheatListTab.MouseDown += new EventHandler(CheatListTab_MouseDown);
                    CheatListTab.MouseEnter += new EventHandler(CheatListTabs_Enter);
                    CheatListTab.MouseLeave += new EventHandler(CheatListTabs_Leave);

                    this.BeginInvoke((MethodInvoker)delegate { flowLayoutPanel1.Controls.Add(CheatListTab); });
                }

                this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });
            }).Start();
        }

        private void SideBar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (flowLayoutPanel1.Top < 0)
                    flowLayoutPanel1.Top += 25;

                flowLayoutPanel1.PerformLayout();
            }
            else
            {
                if (flowLayoutPanel1.Bottom > 50)
                    flowLayoutPanel1.Top -= 25;

                flowLayoutPanel1.PerformLayout();
            }
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                int buttonIndex = (int)flowLayoutPanel1.Controls.Cast<BunifuFlatButton>().ToList().Find(control => control.selected).Tag;

                Console.WriteLine("Selected Index = " + buttonIndex);

                Product product = products.Find(temp_product => temp_product.Id == buttonIndex);

                Console.WriteLine("Product ID: " + product.Id + ", " + product.Name);

                if (product.Id <= 0 || product.Name == null || product.File == null || product.ProcessName == null)
                    return;

                if (product.Status == 0)
                {
                    MessageBox.Show("We're sorry, this cheat is being updated by the Development Team. Check back later, Thanks.");

                    return;
                }

                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                if (Directory.Exists(Environment.CurrentDirectory + "\\" + product.Name + "\\"))
                {
                    try
                    {
                        Directory.Delete(Environment.CurrentDirectory + "\\" + product.Name + "\\");
                    }
                    catch
                    {

                    }
                }

                this.TopMost = false;

                MemoryStream download = client.Download(token, product);

                if (!Directory.Exists(Environment.CurrentDirectory + "\\" + product.Name + "\\"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + product.Name + "\\");

                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                }
                else
                {
                    try
                    {
                        Directory.Delete(Environment.CurrentDirectory + "\\" + product.Name + "\\", true);
                    }
                    catch (Exception ex)
                    {
                        try { Process.GetProcessesByName("run").ToList().ForEach(process => process.Kill()); } catch (Exception ex2) { MessageBox.Show("Failed to Terminate run.exe, run.exe is being used and cannot be redownloaded or updated!"); Console.WriteLine(ex2.ToString()); }

                        Console.WriteLine("[" + ((Process.GetProcessesByName("run.exe").ToList().Count > 0) ? "HANDLED" : "UNHANDLED") + " ERROR]:\n" + ex.ToString());
                    }

                    Thread.Sleep(500);

                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + product.Name + "\\");

                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                }

                File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Normal);

                ZipArchive archive = new ZipArchive(download, ZipArchiveMode.Read);
                archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + product.Name + "\\");
                archive.Dispose();

                Console.WriteLine("Cheat Preloaded - Waiting 120s to Auto Inject");

                //Check if Process has loaded enough memory that we can likely load successfuly
                if (File.Exists(Environment.CurrentDirectory + "\\" + product.Name + "\\run.exe"))
                {
                    //Handle File Attributes
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\run.exe", FileAttributes.Normal);
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name, FileAttributes.Normal);

                    //Start Cheat
                    Console.WriteLine("Starting Cheat");

                    Process process = new Process();

                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.CurrentDirectory + "\\" + product.Name + "\\",
                        FileName = Environment.CurrentDirectory + "\\" + product.Name + "\\run.exe",
                        Arguments = token.AuthToken + " " + product.Id,
                        RedirectStandardOutput = false,
                        //UseShellExecute = false,
                        Verb = "runas"
                    };

                    process.StartInfo = startInfo;
                    process.Start();

                    //Console.WriteLine(process.StandardOutput.ReadToEnd());

                    Console.WriteLine("Started Cheat");

                    //Handle File Attributes
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\run.exe", FileAttributes.Hidden | FileAttributes.System);
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name, FileAttributes.Hidden | FileAttributes.System);

                    //Finish Up
                    Thread.Sleep(1000);

                    this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });

                    if (product.Internal)
                        MessageBox.Show("Injector: " + ((Process.GetProcessesByName("run").Length > 0) ? "Pre-Loaded! You have 60 seconds to start your game!" : "Failed to Launch\n( Make sure your AntiVirus is Disabled and that you start your game AFTER you Inject )"));
                    else
                        MessageBox.Show(
                            "External Execution: " + ((Process.GetProcessesByName("run").Length > 0) ? "Successful" : "Failed" +
                            "\n- Make sure your game is Fullscreen Windowed or Windowed" +
                            "\n- Make sure any Security or AntiVirus's you have are 'FULLY' disabled" +
                            "\n- If you're on Windows 7, set your Windows 7 Desktop Theme as an 'Aero' Theme" +
                            "\n\nNote: In 'UNSUAL' cases, you 'MAY' need to 'UNINSTALL' your AntiVirus as it is possible some AntiVirus's will not 'DISABLE' completely even if the AntiVirus is set as 'DISABLED'."));

                    /*
                    if (Login.ProcessChecks.IsAlive || Login.ProcessChecks.IsBackground)
                        Login.ProcessChecks.Abort();
                    */
                }
                else
                    MessageBox.Show("Injection Error: {MissingInjector}");

                this.Close();

                Environment.Exit(0);
            }).Start();
        }

        #region Event Handlers - Front End
        private void ShowLogs_Click(object sender, EventArgs e)
        {

        }

        #region Sliding Panel Events
        private void slidingPanel_Click(object sender, EventArgs e)
        {
            if (sideBar.Width == 50)
            {
                sideBar.Visible = false;
                sideBar.Width = 300;

                bunifuTransition1.ShowSync(sideBar);
                bunifuTransition2.ShowSync(memberAvatar);
            }
            else
            {
                bunifuTransition2.Hide(memberAvatar);

                sideBar.Visible = false;
                sideBar.Width = 50;

                bunifuTransition1.ShowSync(sideBar);
            }
        }
        #endregion

        #region CheatList Events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Scroll to Move Down/Up
            if (keyData == Keys.Down)
            {
                if (flowLayoutPanel1.Bottom > 50)
                    flowLayoutPanel1.Top -= 25;

                flowLayoutPanel1.PerformLayout();
            }
            else if (keyData == Keys.Up)
            {
                if (flowLayoutPanel1.Top < 0)
                    flowLayoutPanel1.Top += 25;

                flowLayoutPanel1.PerformLayout();
            }

            return true;
        }

        private void CheatListTab_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();

            BunifuFlatButton button = ((BunifuFlatButton)sender);

            foreach (BunifuFlatButton control in flowLayoutPanel1.Controls)
                if (control.selected && button != control)
                    control.selected = false;
        }

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
                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                int ProductID = (int)((BunifuFlatButton)sender).Tag;

                ZipArchive archive = new ZipArchive(client.Download(token, products.Find(product => product.Id == ProductID)), ZipArchiveMode.Read);
                archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + products.Find(product => product.Id == ProductID).Name + "\\");
                archive.Dispose();

                this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });
            }).Start();
        }

        private void CheatListTabs_Enter(object sender, EventArgs e)
        {
            ((BunifuFlatButton)sender).IconZoom = ((BunifuFlatButton)sender).IconZoom + 10;
        }

        private void CheatListTabs_Leave(object sender, EventArgs e)
        {
            ((BunifuFlatButton)sender).IconZoom = ((BunifuFlatButton)sender).IconZoom - 10;
        }
        #endregion

        #endregion

        #region Panel - Move
        private Point MouseDownLocation;

        private void topBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void topBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }
        #endregion

        private void BugReport_MouseEnter(object sender, EventArgs e)
        {
            BugReport.ForeColor = Color.FromArgb(0, 102, 204);
        }

        private void BugReport_MouseLeave(object sender, EventArgs e)
        {
            BugReport.ForeColor = Color.White;
        }

        private void BugReport_Click(object sender, EventArgs e)
        {
            Process.Start("https://maverickcheats.eu/community/forum/23-bug-report/");
        }
    }
}
