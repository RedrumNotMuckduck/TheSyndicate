using System;
using System.Runtime.InteropServices;

namespace TheSyndicate
{
    class ConsoleWindow
    {
        [DllImport("libc")]
        private static extern int system(string exec);

        public static void MaximizeWindow()
        {
            system(@"printf '\e[8;200;200t'");
        }
    }
}
