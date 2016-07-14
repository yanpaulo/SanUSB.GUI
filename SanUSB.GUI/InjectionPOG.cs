using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanUSB.GUI
{

    public delegate void InjectionDelegate(string path);
    public delegate string StartProcessDelegate(string path, string args);

    public static class InjectionPOG
    {
        public static InjectionDelegate OpenFile { get; set; }

        public static StartProcessDelegate StartProcess { get; set; }

        public static string ToolPath { get; set; }
    }
}
