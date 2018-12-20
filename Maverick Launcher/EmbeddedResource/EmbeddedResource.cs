using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Diagnostics;

public class EmbeddedResource
{
    public static Dictionary<string, Assembly> EmbeddedAssemblies = new Dictionary<string, Assembly>();
    public static Dictionary<string, byte[]> EmbeddedResources = new Dictionary<string, byte[]>();

    public static void LoadResource(string embeddedResource, string fileName)
    {
        byte[] ba = null;
        Assembly asm = null;
        Assembly curAsm = Assembly.GetExecutingAssembly();

        using (Stream stm = curAsm.GetManifestResourceStream(embeddedResource))
        {
            // Either the file is not existed or it is not mark as embedded resource
            if (stm == null)
                throw new Exception(embeddedResource + " is not found in Embedded Resources.");

            // Get byte[] from the file from embedded resource
            ba = new byte[(int)stm.Length];
            stm.Read(ba, 0, (int)stm.Length);
            
            // Add the assembly/dll into dictionary
            EmbeddedResources.Add(fileName, ba);
        }
    }

    public static void LoadAssembly(string embeddedAssembly, string fileName)
    {
        byte[] ba = null;
        Assembly asm = null;
        Assembly curAsm = Assembly.GetExecutingAssembly();

        using (Stream stm = curAsm.GetManifestResourceStream(embeddedAssembly))
        {
            // Either the file is not existed or it is not mark as embedded resource
            if (stm == null)
                throw new Exception(embeddedAssembly + " is not found in Embedded Resources.");

            // Get byte[] from the file from embedded resource
            ba = new byte[(int)stm.Length];
            stm.Read(ba, 0, (int)stm.Length);

            try
            {
                asm = Assembly.Load(ba);

                // Add the assembly/dll into dictionary
                EmbeddedAssemblies.Add(asm.FullName, asm);
                return;
            }
            catch
            {
                // Purposely do nothing
                // Unmanaged dll or assembly cannot be loaded directly from byte[]
                // Let the process fall through for next part
            }
        }
    }

    public static void ExecuteAssembly(string embeddedAssembly, string fileName)
    {
        byte[] ba = null;
        Assembly asm = null;
        Assembly curAsm = Assembly.GetExecutingAssembly();

        using (Stream stm = curAsm.GetManifestResourceStream(embeddedAssembly))
        {
            // Either the file is not existed or it is not mark as embedded resource
            if (stm == null)
                throw new Exception(embeddedAssembly + " is not found in Embedded Resources.");

            // Get byte[] from the file from embedded resource
            ba = new byte[(int)stm.Length];
            stm.Read(ba, 0, (int)stm.Length);

            try
            {
                asm = Assembly.Load(ba);

                MethodInfo main = asm.EntryPoint;
                main.Invoke(null, new object[] { null });

                Console.WriteLine("Invoked Updater.exe");

                // Add the assembly/dll into dictionary
                EmbeddedAssemblies.Add(asm.FullName, asm);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }

    public static Assembly Get(string assemblyFullName)
    {
        if (EmbeddedAssemblies.ContainsKey(assemblyFullName))
            return EmbeddedAssemblies[assemblyFullName];

        return null;
    }
}