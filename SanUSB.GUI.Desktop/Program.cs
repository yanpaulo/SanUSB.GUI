using System;
using System.Linq;
using Eto;
using Eto.Forms;
using System.Diagnostics;
using System.IO;

namespace SanUSB.GUI.Desktop
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            InitPOG();
            new Application(Platform.Detect).Run(new MainForm());
        }

        private static void InitPOG()
        {
            InjectionPOG.ToolPath = FindToolPath();
            InjectionPOG.OpenFile = (f) => System.Diagnostics.Process.Start(f);
            InjectionPOG.StartProcess = (f, a) =>
            {
                Process p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = f,
                        Arguments = a,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }
                };
                p.Start();
                var ret = p.StandardOutput.ReadToEnd().Replace(f, "");
                p.WaitForExit();
                return ret;
            };
        }

        private static string FindToolPath()
        {
            string path = File.ReadAllLines("tool-path-list.txt").FirstOrDefault(l => File.Exists(l));
            return path;
        }
    }
}
