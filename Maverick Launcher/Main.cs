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
using System.Reflection;

namespace Main
{
    public partial class Main : Form
    {
        public bool Busy = false;

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
            
            loading.Image = Image.FromStream(new MemoryStream(EmbeddedResource.EmbeddedResources.First(resource => resource.Key == "Logo (40x40).gif").Value));

            sideBar.Width = 50;

            bunifuTransition2.Show(memberAvatar);

            memberAvatar.Image = token.Member.Avatar;
            memberUsername.Text = token.Member.Username;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Busy = true;

            this.sideBar.MouseWheel += SideBar_MouseWheel;

            new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                object Output;

                if (client.Products(token, out Output))
                {
                    products = (List<Product>)Output;

                    foreach (Product product in products)
                    {
                        Logs.LogEntries.Add(product.Id + ", " + product.Name + "," + product.Image.LongLength);

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
                }
                else
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.requestFailed.Text = (string)Output;
                        this.requestFailed.Visible = true;
                    });
                }

                this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });

                Busy = false;
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
            if (Busy)
                return;

            Busy = true;

            new Thread(() =>
            {
                if (!flowLayoutPanel1.Controls.Cast<BunifuFlatButton>().ToList().Any(control => control.selected))
                    return;

                int buttonIndex = (int)flowLayoutPanel1.Controls.Cast<BunifuFlatButton>().ToList().Find(control => control.selected).Tag;

                Logs.LogEntries.Add("Selected Index = " + buttonIndex);

                Product product = products.Find(temp_product => temp_product.Id == buttonIndex);

                Logs.LogEntries.Add("Product ID: " + product.Id + ", " + product.Name);

                if (product.Id <= 0 || product.Name == null || product.File == null || product.ProcessName == null)
                    return;

                if (product.Status == 0)
                {
                    MessageBox.Show("We're sorry, this cheat is being updated by the Development Team. Check back later, Thanks.");

                    Busy = false;

                    return;
                }

                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                this.BeginInvoke((MethodInvoker)delegate { this.TopMost = false; });

                object Output;

                if (client.Download(token, product, out Output))
                {
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\" + product.Name + "\\"))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + product.Name + "\\");

                        File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                    }
                    else
                    {
                        try
                        {
                            foreach (string FilePath in Directory.GetFiles(Environment.CurrentDirectory + "\\" + product.Name + "\\"))
                            {
                                try
                                {
                                    File.Delete(FilePath);
                                }
                                catch
                                {
                                    try
                                    {
                                        Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FilePath)).ToList().ForEach(process => process.Kill());
                                    }
                                    catch (Exception ex2)
                                    {
                                        MessageBox.Show("Failed to Terminate " + Path.GetFileNameWithoutExtension(FilePath) + ", run.exe is being used and cannot be redownloaded or updated!");

                                        Logs.LogEntries.Add(ex2.ToString());
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logs.LogEntries.Add("Error: Directory unable to be deleted\n" + ex.ToString());
                        }

                        Thread.Sleep(500);

                        File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                    }
                }
                else
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.requestFailed.Text = (string)Output;
                        this.requestFailed.Visible = true;
                    });

                    this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });

                    Busy = false;

                    return;
                }

                File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\", FileAttributes.Normal);

                string FileName = Path.GetRandomFileName() + ".exe";

                using (ZipArchive Archive = new ZipArchive((MemoryStream)Output, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry Entry in Archive.Entries)
                    {
                        if (!File.Exists(Entry.FullName))
                        {
                            if (Entry.Name.ToLower().Contains("run"))
                            {
                                using (FileStream FileStream = new FileStream(Environment.CurrentDirectory + "\\" + product.Name + "\\" + Entry.Name.Replace("run.exe", FileName), FileMode.Create, FileAccess.Write))
                                {
                                    using (Stream Stream = Entry.Open())
                                    {
                                        Stream.CopyTo(FileStream);
                                    }
                                }
                            }
                            else
                            {
                                Entry.ExtractToFile(Environment.CurrentDirectory + "\\" + product.Name + "\\" + Entry.Name);
                            }
                        }
                    }
                }

                //Check if Process has loaded enough memory that we can likely load successfuly
                if (File.Exists(Environment.CurrentDirectory + "\\" + product.Name + "\\" + FileName))
                {
                    //Handle File Attributes
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\" + FileName, FileAttributes.Normal);
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name, FileAttributes.Normal);

                    if (!product.Internal)
                    {
                        if (!Directory.Exists(@"C:\WINDOWS\Microsoft.NET\assembly\GAC_64\SlimDX\") || !Directory.GetFiles(@"C:\WINDOWS\Microsoft.NET\assembly\GAC_64\SlimDX\", "*.*", SearchOption.AllDirectories).Any(file => file.Contains("SlimDX.dll")))
                        {
                            Logs.LogEntries.Add("Installing DirectX Library");

                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                this.requestFailed.Text = "Extracting DirectX Library...";
                                this.requestFailed.Visible = true;
                            });

                            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Main.Resources.SlimDX.msi"))
                            {
                                using (FileStream fileStream = new FileStream("SlimDX.msi", FileMode.OpenOrCreate))
                                {
                                    for (int i = 0; i < stream.Length; i++)
                                        fileStream.WriteByte((byte)stream.ReadByte());
                                }
                            }

                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                this.requestFailed.Text = "Installing DirectX Library...";
                                this.requestFailed.Visible = true;
                            });

                            ProcessStartInfo msiexec = new ProcessStartInfo()
                            {
                                FileName = "msiexec",
                                WorkingDirectory = Environment.CurrentDirectory + "\\" + product.Name + "\\",
                                Arguments = " /i \"" + Environment.CurrentDirectory + "\\SlimDX.msi\" /q",
                                Verb = "runas"
                            };

                            Process.Start(msiexec).WaitForExit();

                            Logs.LogEntries.Add("Installed DirectX Library");

                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                this.requestFailed.Text = "Installed DirectX Library";
                                this.requestFailed.Visible = true;
                            });

                            try { File.Delete(Environment.CurrentDirectory + "\\SlimDX.msi"); } catch (Exception ex) { Logs.LogEntries.Add("Failed to Delete the File: SlimDX.msi" + "\nERROR:\n" + ex.ToString()); }
                        }
                    }

                    //Start Cheat
                    Logs.LogEntries.Add("Starting Cheat");

                    Process process = new Process();

                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.CurrentDirectory + "\\" + product.Name + "\\",
                        FileName = Environment.CurrentDirectory + "\\" + product.Name + "\\" + FileName,
                        Arguments = token.AuthToken + " " + product.Id,
                        RedirectStandardOutput = false,
                        //UseShellExecute = false,
                        Verb = "runas"
                    };

                    process.StartInfo = startInfo;
                    process.Start();

                    //Logs.LogEntries.Add(process.StandardOutput.ReadToEnd());

                    Logs.LogEntries.Add("Started Cheat");

                    //Handle File Attributes
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name + "\\" + FileName, FileAttributes.Hidden | FileAttributes.System);
                    File.SetAttributes(Environment.CurrentDirectory + "\\" + product.Name, FileAttributes.Hidden | FileAttributes.System);

                    //Finish Up
                    Thread.Sleep(2500);

                    if (product.Internal)
                        MessageBox.Show("Injector: " + ((Process.GetProcessesByName(FileName.Replace(".exe", "")).Length > 0) ? "Pre-Loaded! You have 60 seconds to start your game!" : "Failed to Launch\n( Make sure your AntiVirus is Disabled and that you start your game AFTER you Inject )"));
                    else
                        MessageBox.Show(
                            "External Execution: " + ((Process.GetProcessesByName(FileName.Replace(".exe", "")).Length > 0) ? 
                            "Successful" +
                            "\nPlease Launch your Game"
                            : 
                            "Failed"
                            +
                            "\n- Make sure your game is Fullscreen Windowed or Windowed" +
                            "\n- Make sure any Security or AntiVirus's you have are 'FULLY' disabled" +
                            "\n- If you're on Windows 7, set your Windows 7 Desktop Theme as an 'Aero' Theme" +
                            "\n\nNote: In 'UNSUAL' cases, you 'MAY' need to 'UNINSTALL' your AntiVirus as it is possible some AntiVirus's will not 'DISABLE' completely even if the AntiVirus is set as 'DISABLED'."));

                    /*
                    if (Login.ProcessChecks.IsAlive || Login.ProcessChecks.IsBackground)
                        Login.ProcessChecks.Abort();
                    */

                    this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });
                }
                else
                    MessageBox.Show("Injection Error: {MissingInjector}");

                this.BeginInvoke((MethodInvoker)delegate { this.Close(); });

                Environment.Exit(0);

                Busy = false;
            }).Start();
        }

        #region Event Handlers - Front End
        private void ShowLogs_Click(object sender, EventArgs e)
        {
            new Logs().ShowDialog();
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

            if (Busy)
                return;

            Busy = true;

            new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                Product Product = products.Find(product => product.Id == (int)((BunifuFlatButton)sender).Tag);

                Logs.LogEntries.Add("Product ID: " + Product.Id + ", " + Product.Name);

                if (Product.Id <= 0 || Product.Name == null || Product.File == null || Product.ProcessName == null)
                    return;

                if (Product.Status == 0)
                {
                    MessageBox.Show("We're sorry, this cheat is being updated by the Development Team. Check back later, Thanks.");

                    Busy = false;

                    return;
                }

                this.BeginInvoke((MethodInvoker)delegate { bunifuTransition3.ShowSync(loading); });

                this.BeginInvoke((MethodInvoker)delegate { this.TopMost = false; });

                if (!Directory.Exists(Environment.CurrentDirectory + "\\" + Product.Name + "\\"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + Product.Name + "\\");

                    File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                }
                else
                {
                    try
                    {
                        foreach (string FilePath in Directory.GetFiles(Environment.CurrentDirectory + "\\" + Product.Name + "\\"))
                        {
                            try
                            {
                                File.Delete(FilePath);
                            }
                            catch
                            {
                                try
                                {
                                    Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FilePath)).ToList().ForEach(process => process.Kill());
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show("Failed to Terminate " + Path.GetFileNameWithoutExtension(FilePath) + ", run.exe is being used and cannot be redownloaded or updated!");

                                    Logs.LogEntries.Add(ex2.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.LogEntries.Add("Error: Directory unable to be deleted\n" + ex.ToString());
                    }

                    Thread.Sleep(500);

                    File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name + "\\", FileAttributes.Hidden | FileAttributes.System);
                }

                object Output;

                if (client.Download(token, Product, out Output))
                {
                    string FileName = Path.GetRandomFileName() + ".exe";

                    using (ZipArchive Archive = new ZipArchive((MemoryStream)Output, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry Entry in Archive.Entries)
                        {
                            if (Entry.Name.ToLower().Contains("run"))
                            {
                                using (FileStream FileStream = new FileStream(Environment.CurrentDirectory + "\\" + Product.Name + "\\" + Entry.Name.Replace("run", FileName), FileMode.Create, FileAccess.Write))
                                {
                                    using (Stream Stream = Entry.Open())
                                    {
                                        Stream.CopyTo(FileStream);
                                    }
                                }
                            }
                            else
                            {
                                Archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + Product.Name + "\\");
                            }
                        }
                    }

                    Logs.LogEntries.Add("Cheat Preloaded - Waiting 120s to Auto Inject");

                    //Check if Process has loaded enough memory that we can likely load successfuly
                    if (File.Exists(Environment.CurrentDirectory + "\\" + Product.Name + "\\" + FileName))
                    {
                        //Handle File Attributes
                        File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name + "\\" + FileName, FileAttributes.Normal);
                        File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name, FileAttributes.Normal);

                        //Start Cheat
                        Logs.LogEntries.Add("Starting Cheat");

                        Process process = new Process();

                        ProcessStartInfo startInfo = new ProcessStartInfo()
                        {
                            WorkingDirectory = Environment.CurrentDirectory + "\\" + Product.Name + "\\",
                            FileName = Environment.CurrentDirectory + "\\" + Product.Name + "\\" + FileName,
                            Arguments = token.AuthToken + " " + Product.Id,
                            RedirectStandardOutput = false,
                            //UseShellExecute = false,
                            Verb = "runas"
                        };

                        process.StartInfo = startInfo;
                        process.Start();

                        //Logs.LogEntries.Add(process.StandardOutput.ReadToEnd());

                        Logs.LogEntries.Add("Started Cheat");

                        //Handle File Attributes
                        File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name + "\\" + FileName, FileAttributes.Hidden | FileAttributes.System);
                        File.SetAttributes(Environment.CurrentDirectory + "\\" + Product.Name, FileAttributes.Hidden | FileAttributes.System);

                        //Finish Up
                        Thread.Sleep(1000);

                        this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });

                        if (Product.Internal)
                            MessageBox.Show("Injector: " + ((Process.GetProcessesByName(FileName.Replace(".exe", "")).Length > 0) ? "Pre-Loaded! You have 60 seconds to start your game!" : "Failed to Launch\n( Make sure your AntiVirus is Disabled and that you start your game AFTER you Inject )"));
                        else
                            MessageBox.Show(
                                "External Execution: " + ((Process.GetProcessesByName(FileName.Replace(".exe", "")).Length > 0) ? "Successful" : "Failed" +
                                "\n- Make sure your game is Fullscreen Windowed or Windowed" +
                                "\n- Make sure any Security or AntiVirus's you have are 'FULLY' disabled" +
                                "\n- If you're on Windows 7, set your Windows 7 Desktop Theme as an 'Aero' Theme" +
                                "\n\nNote: In 'UNSUAL' cases, you 'MAY' need to 'UNINSTALL' your AntiVirus as it is possible some AntiVirus's will not 'DISABLE' completely even if the AntiVirus is set as 'DISABLED'."));

                        /*
                        if (Login.ProcessChecks.IsAlive || Login.ProcessChecks.IsBackground)
                            Login.ProcessChecks.Abort();
                        */

                        this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });
                    }
                    else
                        MessageBox.Show("Injection Error: {MissingInjector}");
                }
                else
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.requestFailed.Text = (string)Output;
                        this.requestFailed.Visible = true;
                    });

                    this.BeginInvoke((MethodInvoker)delegate { loading.Visible = false; });

                    Busy = false;

                    return;
                }

                this.BeginInvoke((MethodInvoker)delegate { this.Close(); });

                Environment.Exit(0);

                Busy = false;
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
            Process.Start("https://MaverickCheats.net/community/forum/23-bug-report/");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void memberAvatar_Click(object sender, EventArgs e)
        {
            Process.Start("https://MaverickCheats.net/community/profile/" + token.Member.UserID + "-" + token.Member.Username);
        }
    }
}
