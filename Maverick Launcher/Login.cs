using Main.AuthLib;
using Main.Functions;
using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class Login : Form
    {
        public Client client = new Client();

        public static string Version = "0.89.96.3";

        public static Token token;

        #region Login Init
        public Login()
        {
            InitializeComponent();

            Console.WriteLine("Form Loaded");

            //Auto Login?????
            if (File.Exists("autologin.data"))
            {
                if (File.ReadAllLines("autologin.data").Length < 4)
                {
                    try
                    {
                        File.Delete("autologin.data");
                    }
                    catch
                    {
                        MessageBox.Show("AutoLogin File cannot be removed");
                    }

                    return;
                }

                Console.WriteLine("Loading AutoLogin Data");

                //Load Config
                StreamReader sr = new StreamReader("autologin.data");
                string useAutoLogin = sr.ReadLine();
                string forumLogin = sr.ReadLine();
                string userHash = sr.ReadLine();
                string pwHash = sr.ReadLine();

                sr.Close();
                sr.Dispose();

                //Setup Decryption...etc
                rememberMeCheckBox.Checked = !Convert.ToBoolean(useAutoLogin);
                autoLoginCheckBox.Checked = Convert.ToBoolean(useAutoLogin);

                Console.WriteLine("Decrypting AutoLogin Data");

                userHash = Encryption.decrypt(userHash);
                pwHash = Encryption.decrypt(pwHash);

                Console.WriteLine("Decrypted AutoLogin Data");

                if (userHash == "" || pwHash == "")
                {
                    Password.isPassword = false;

                    Username.Text = "Username";
                    Password.Text = "Password";

                    Console.WriteLine("Username and Password are not included in the AutoLogin File!");
                }
                else
                {
                    Password.isPassword = true;

                    Username.Text = userHash;
                    Password.Text = pwHash;
                }

                if (Convert.ToBoolean(useAutoLogin) && !Convert.ToBoolean(forumLogin))
                {
                    Token TempToken = new Token();

                    if (client.Login(Username.Text, Password.Text, "", ref TempToken))
                    {
                        token = TempToken;

                        Console.WriteLine("Login Found-" + token);

                        new Main(client, token).ShowDialog(); //Shows Dialog on this Thread, Thread is held up and wont display Login Form
                    }
                    else
                    {
                        //Error - Display Response
                    }
                }
                /*
                #region Forum Login - Unfinished
                else if (Convert.ToBoolean(useAutoLogin) && Convert.ToBoolean(forumLogin))
                {
                    Console.WriteLine("Attempting to AutoLogin with OAuth");

                    string PrivateKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("+", "").Replace("=", "").Replace(@"/", "");

                    Process process = Process.Start("https://maverickcheats.eu/community/OAuth.php?PrivateKey=" + PrivateKey + "&HWID=" + FingerPrint.Value());

                    new Thread(() =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string Response = client.OAuth_Finish(PrivateKey, ref token);

                            if (Response == "Login Found")
                            {
                                Console.WriteLine("Login Found-" + token);

                                new Thread(() => new Main(client, token).ShowDialog()).Start();

                                break;
                            }

                            Thread.Sleep(2500);
                        }
                    }
                    ).Start();
                }
                #endregion
                */
                else
                {
                    rememberMeCheckBox.Checked = true;

                    Username.Text = userHash;
                    Password.Text = pwHash;
                }
            }
        }
        #endregion

        #region UI events
        #region Login Load()
        private void Login_Load(object sender, EventArgs e)
        {
            if (Version != client.Version())
            {
                MessageBox.Show("Updating! - 'Updater.exe'");

            retry:
                //Kill Process's
                foreach (Process process in new List<Process>(Process.GetProcesses().Where(process => process.ProcessName.ToLower() == "updater" || process.ProcessName.ToLower() == "run")))
                    try { process.CloseMainWindow(); process.Close(); process.Kill(); } catch { }

                //Starting Updates
                Console.WriteLine("Starting Update Protocol");

                //Extract the Update and only get the Updater.exe
                using (ZipArchive archive = new ZipArchive(client.Update(), ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry file in archive.Entries)
                    {
                        if (File.Exists(Environment.CurrentDirectory + "\\" + file.Name))
                            try { File.Delete(Environment.CurrentDirectory + "\\" + file.Name); } catch (Exception ex) { Console.WriteLine("Failed to Delete the File: " + file.FullName + "\nERROR:\n" + ex.ToString()); continue; }

                        file.ExtractToFile(Environment.CurrentDirectory + "\\" + file.Name);

                        Console.WriteLine("Extracting File: " + file.Name);
                    }
                }

                if (File.Exists(Environment.CurrentDirectory + "\\Updater.exe"))
                {
                    Console.WriteLine("Updater Executable Found!");

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.CurrentDirectory + "\\",
                        //UseShellExecute = false,
                        FileName = Environment.CurrentDirectory + "\\" + "Updater.exe",
                        //WindowStyle = ProcessWindowStyle.Hidden,
                        //CreateNoWindow = true,
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                }
                else
                {
                    Console.WriteLine("AutoUpdater Executable Missing! -> Recovery: Trying to download AutoUpdater");

                    goto retry;
                }

                Environment.Exit(0);
            }
        }
        #endregion

        #region Login Button Events
        private void loginButton_Click(object sender, EventArgs e)
        {
            Token token = new Token();

            if (client.Login(Username.Text, Password.Text, "", ref token))
            {
                //Check if Either is Enabled or Disabled
                if (File.Exists("autologin.data"))
                    try
                    {
                        File.Delete("autologin.data");
                    }
                    catch
                    {
                        MessageBox.Show("Auto Login File cannot be removed");
                    }

                #region AutoLogin
                if (autoLoginCheckBox.Checked)
                {
                    StreamWriter sw = new StreamWriter("autologin.data");
                    sw.WriteLine("True");
                    sw.WriteLine("False");
                    sw.WriteLine(Encryption.crypt(Username.Text));
                    sw.WriteLine(Encryption.crypt(Password.Text));
                    sw.Close();
                }
                else if (rememberMeCheckBox.Checked)
                {
                    StreamWriter sw = new StreamWriter("autologin.data");
                    sw.WriteLine("False");
                    sw.WriteLine("False");
                    sw.WriteLine(Encryption.crypt(Username.Text));
                    sw.WriteLine(Encryption.crypt(Password.Text));
                    sw.Close();
                }
                else
                {
                    if (File.Exists("autologin.data"))
                    {
                        try
                        {
                            File.Delete("autologin.data");
                        }
                        catch
                        {
                            MessageBox.Show("Auto Login File cannot be removed");
                        }
                    }
                }
                #endregion

                new Main(client, token).Show();

                Hide();
            }
        }
        #endregion

        #region Username/Password Event Handlers
        private void Username_Enter(object sender, EventArgs e)
        {
            if (Username.Text == "" || Username.Text == "Username")
            {
                Username.Text = "";
            }
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = "Username";
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            if (Password.Text == "" || Password.Text == "Password")
            {
                Password.isPassword = true;
                Password.Text = "";
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (Password.Text == "")
            {
                Password.isPassword = false;
                Password.Text = "Password";
            }
        }
        #endregion

        #region Drag Control - Form
        private Point MouseDownLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }
        #endregion
        #endregion
    }
}
