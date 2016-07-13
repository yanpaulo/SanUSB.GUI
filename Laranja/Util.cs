using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laranja
{
    public class Util
    {
        public static void Open(string location) => System.Diagnostics.Process.Start(location);
    }
}
