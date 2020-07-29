using System;
using System.Diagnostics;
using System.IO;

namespace auto_uninstall_edge
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = string.Format(@"{0}\Program Files (x86)\Microsoft\Edge\Application", 
                            Environment.GetEnvironmentVariable("SystemRoot").Split('\\')[0]); // {0}==sysRoot/windir, should return just the C: part
            if (!Directory.Exists(dir))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Edge could not be detected on its default path ({0})", dir);
                Console.ResetColor();
                Console.Write("Enter Edge install path (default: \"{0}\"): ", dir);
                dir = Console.ReadLine();
                dir = dir.Trim('"');
                if (!Directory.Exists(dir))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Path does not exist! Exiting!");
                    Console.ResetColor();
                    return;
                }
            }
            const string argv = "--uninstall --system-level --verbose-logging --force-uninstall";
            string path = "Installer\\setup.exe";

            string[] sub = Directory.GetDirectories(dir);
            int i = 0;
            for(; i < sub.Length; i++)
                if (char.IsDigit(Path.GetFileName(sub[i])[0])) break; // breaks on XX.XX... (version directory)
            path = Path.Combine(sub[i], path);

            ProcessStartInfo psi = new ProcessStartInfo(path, argv) {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };

            Process p = new Process() { StartInfo = psi };
            Console.WriteLine("Starting uninstaller... (this may take some while)");
            p.Start();
            string stdOut = p.StandardOutput.ReadToEnd();
            string stdErr = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            p.Dispose();
            if (!string.IsNullOrEmpty(stdOut) || !string.IsNullOrEmpty(stdErr))
            {
                Console.WriteLine("----- Uninstaller output -----");
                Console.WriteLine("StdOut: {0}", string.IsNullOrEmpty(stdOut) ? "(nothing)" : stdOut);
                Console.WriteLine();
                Console.WriteLine("StdErr: {0}", string.IsNullOrEmpty(stdErr) ? "(nothing)" : stdErr);
                Console.WriteLine("----- Uninstaller output -----");
            }
            Console.WriteLine("Edge should be uninstalled now! Press any key to exit.");
            Console.ReadKey();
        }
    }
}
