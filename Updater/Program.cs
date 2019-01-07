using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using Updater.AuthLib;

namespace Updater
{
    public class Program
    {
        public static Dictionary<string, Assembly> dic = null;

        public static Client client = new Client();

        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            //Get embedded Theme.dll if it isnt on the Disk
            EmbeddedAssembly.Load("Updater.Resources.Theme.dll", "Theme.dll");

            //Get embedded Theme.dll if it isnt on the Disk
            EmbeddedAssembly.Load("Updater.Resources.NetworkTypes.dll", "NetworkTypes.dll");

        Retry:
            //Kill Process's
            foreach (Process process in new List<Process>(Process.GetProcesses().Where(process => process.ProcessName.ToLower() == "maverickclient" || process.ProcessName.ToLower() == "run" || (process.ProcessName.ToLower() == "updater" && process.Id != Process.GetCurrentProcess().Id))))
                try { process.CloseMainWindow(); process.Close(); process.Kill(); } catch {  }

            //Starting Updates
            Debug.WriteLine("Starting Update Protocol");

            Debug.WriteLine("Local Loader Outdated, downloaded new loader in current folder!");

            //Delete client to update client
            if (File.Exists(Environment.CurrentDirectory + "\\" + "MaverickClient.exe"))
            {
                try
                {
                    File.Delete(Environment.CurrentDirectory + "\\" + "MaverickClient.exe");
                }
                catch
                {
                    Console.WriteLine("Failed to Delete MaverickClient.exe!");

                    Console.ReadLine();
                }
            }

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

            //Starts Launcher
            Console.WriteLine("Starting Launcher!");

            if (File.Exists(Environment.CurrentDirectory + "\\" + "MaverickClient.exe"))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = Environment.CurrentDirectory + "\\",
                    //UseShellExecute = false,
                    FileName = Environment.CurrentDirectory + "\\" + "MaverickClient.exe",
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    //CreateNoWindow = true,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                process.StartInfo = startInfo;
                process.Start();

                Environment.Exit(0);
            }
            else
            {
                goto Retry;
            }
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
