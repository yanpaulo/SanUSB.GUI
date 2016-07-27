using System;
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
            string path = null;
            if (File.Exists("tool-path.txt"))
            {
                path = File.ReadAllText("tool-path.txt");
            }
            else
            {
                var lines = File.ReadAllLines("tool-path-list.txt");
                foreach (var item in lines)
                {
                    if (File.Exists(item))
                    {
                        path = item;

                        var f = File.CreateText("tool-path.txt");
                        f.Write(path);
                        f.Close();

                    }
                }

            }
            return path;
        }
    }
}
