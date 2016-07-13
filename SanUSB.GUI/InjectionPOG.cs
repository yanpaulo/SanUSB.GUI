using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanUSB.GUI
{

    public delegate void InjectionDelegate(string path);

    public class InjectionPOG
    {
        public static InjectionDelegate Delegate { get; set; }
    }
}
