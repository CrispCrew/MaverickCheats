using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Main.Functions
{
    public class FingerPrint
    {
        private static string BIOS = "";
        private static string BASE = "";
        private static string CPU = "";
        private static string VIDEO = "";
        private static string DISKS = "";
        private static string SYSTEM = "";

        public static string HWID = string.Empty;
        
        public static string Value()
        {
            if (string.IsNullOrEmpty(HWID))
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                BIOS = GetHash(biosId());

                Console.WriteLine(BIOS + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                BASE = GetHash(baseId());

                Console.WriteLine(BASE + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                CPU = GetHash(cpuId());

                Console.WriteLine(CPU + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                VIDEO = GetHash(videoId());

                Console.WriteLine(VIDEO + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                SYSTEM = GetHash(OSid());

                Console.WriteLine(SYSTEM + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                DISKS = GetHash(diskId());

                Console.WriteLine(DISKS + " found in " + timer.Elapsed.TotalMilliseconds + "ms");

                HWID = GetHash("BIOS >> " + BIOS + "\nBASE >> " + BASE + "\nCPU >> " + CPU + "\nVIDEO >> " + VIDEO + "\nOS >> " + SYSTEM + "\nDISKS >> " + DISKS, true);

                Console.WriteLine("Hashed all Hardware Serials in " + timer.Elapsed.TotalMilliseconds + "ms");

                Console.WriteLine("Hashed all Hardware Serials: " + HWID);
            }
            else
            {
                Console.WriteLine("Fingerprint Exists");
            }

            return HWID;
        }

        public static string GetHash(string s, bool seperators = false)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);

            if (seperators)
                return GetHexString(sec.ComputeHash(bt));
            else
                return GetHexString(sec.ComputeHash(bt)).Replace("-", "");
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select " + wmiProperty + " From " + wmiClass);
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                try
                {
                    id += mo[wmiProperty].ToString() + ",";
                }
                catch
                {

                }
            }

            return id.TrimEnd(',');
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
                + identifier("Win32_BIOS", "SerialNumber");
        }
        //Motherboard IDw
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Name")
                + identifier("Win32_BaseBoard", "Model")
                + identifier("Win32_BaseBoard", "Product")
                + identifier("Win32_BaseBoard", "Manufacturer")
                + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //CPU Identifier
        private static string cpuId()
        {
            return identifier("Win32_Processor", "Name")
                + identifier("Win32_Processor", "Manufacturer")
                + identifier("Win32_Processor", "ProcessorId");
        }
        //Primary video controller ID
        private static string videoId()
        {   
            return identifier("Win32_VideoController", "Name")
                + identifier("Win32_VideoController", "VideoProcessor")
                + identifier("Win32_VideoController", "PNPDeviceID");
        }
        //Disk IDw
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Index")
                + identifier("Win32_DiskDrive", "Name")
                + identifier("Win32_DiskDrive", "Model")
                + identifier("Win32_DiskDrive", "Partitions")
                + identifier("Win32_DiskDrive", "SerialNumber");
        }
        //OS Identifiers
        private static string OSid()
        {
            return identifier("Win32_OperatingSystem", "Name")
                + identifier("Win32_OperatingSystem", "CSName")
                + identifier("Win32_OperatingSystem", "SerialNumber")
                + identifier("Win32_OperatingSystem", "RegisteredUser");
        }
        #endregion
    }
}