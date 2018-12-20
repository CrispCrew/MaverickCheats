using Main.AuthLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Login : Form
    {
        public static string Version = "0.01";

        public Client client = new Client();

        public Login()
        {
            //Get embedded Theme.dll if it isnt on the Disk
            EmbeddedResource.LoadAssembly("Main.Resources.Bunifu_UI_v1.52.dll", "Bunifu_UI_v1.52.dll");

            InitializeComponent();

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

        private void Login_Load(object sender, EventArgs e)
        {
            Console.WriteLine(client.Version());
        }
    }
}
