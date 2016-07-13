using System;
using Eto;
using Eto.Forms;

namespace SanUSB.GUI.Desktop
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            InjectionPOG.Delegate = (f) => System.Diagnostics.Process.Start(f);
            new Application(Platform.Detect).Run(new MainForm());
        }
    }
}
