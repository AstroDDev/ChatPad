using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration
{
    internal class RuntimeOptions
    {
        public static double DeltaTime;
        public static long Frame;
        public static bool FullPassthrough = false;
        public static bool Paused = true;
    }
}
