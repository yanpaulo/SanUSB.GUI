using System;
using Eto;
using Eto.Forms;
using System.Diagnostics;

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
    }
}
