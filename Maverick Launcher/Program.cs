using Main;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Get embedded Theme.dll if it isnt on the Disk
            EmbeddedResource.LoadAssembly("Main.Resources.Bunifu_UI_v1.52.dll", "Bunifu_UI_v1.52.dll");

            //Get embedded Theme.dll if it isnt on the Disk
            EmbeddedResource.LoadAssembly("Main.Resources.NetworkTypes.dll", "NetworkTypes.dll");

            //Get embedded images if it isnt on the Disk
            EmbeddedResource.LoadResource("Main.Resources.Spinner.gif", "Spinner.gif");

            //Set Debug Log to English
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            //Listener Events for Assembly Resolve and Unhandled Exceptions
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomain.CurrentDomain.UnhandledException += (sender, arg) => HandleUnhandledException(arg.ExceptionObject as Exception);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedResource.Get(args.Name);
        }

        public static void HandleUnhandledException(Exception ex)
        {
            Logs.LogEntries.Add("Exception: " + ex.ToString());
        }
    }
}
