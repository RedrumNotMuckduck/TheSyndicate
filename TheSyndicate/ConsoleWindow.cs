using System;
using System.Runtime.InteropServices;

namespace TheSyndicate
{
    class ConsoleWindow
    {
        public static void MaximizeWindow()
        {
            if (GameEngine.Is_Windows)
            {
                ShowWindow(ConsoleWindow.ThisConsole, ConsoleWindow.MAXIMIZE);
            }
            else
            {
                MaximizeMacOS();
            }
        }

        [DllImport("libc")]
        private static extern int system(string exec);
        private static void MaximizeMacOS()
        {
            system(@"printf '\e[8;200;200t'");
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        public static IntPtr ThisConsole = GetConsoleWindow();
        public const int MAXIMIZE = 3;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
