using System;
using System.Runtime.InteropServices;
using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine
{
    static class ConsoleManager
    {
        public static Coord LastFontSize { get; private set; }
        private static Coord lastDimesions;


        static ConsoleManager()
        {
            LastFontSize = new Coord(8, 12);
            lastDimesions = new Coord(80, 25);

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 SetCurrentConsoleFontEx(
          IntPtr ConsoleOutput,
          bool MaximumWindow,
          ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

        private enum StdHandle
        {
            OutputHandle = -11
        }

        [DllImport("kernel32")]
        private static extern IntPtr GetStdHandle(StdHandle index);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public Coord dwFontSize;
            public int FontFamily;
            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] // Edit sizeconst if the font name is too big
            public string FaceName;
        }

        public static void SetFontSize(Coord dimensions)
        {
            SetFontSize(dimensions.X, dimensions.Y);
        }


        public static void SetFontSize(short X = 8, short Y = 12)
        {
            // Instantiating CONSOLE_FONT_INFO_EX and setting its size (the function will fail otherwise)
            CONSOLE_FONT_INFO_EX ConsoleFontInfo = new CONSOLE_FONT_INFO_EX();
            ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);

            // Optional, implementing this will keep the fontweight and fontsize from changing
            // See notes
            // GetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);

            ConsoleFontInfo.FaceName = "Terminal";
            ConsoleFontInfo.dwFontSize.X = X;
            ConsoleFontInfo.dwFontSize.Y = Y;


            SetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);

            LastFontSize = new Coord(X, Y);
        }

        public static void SetConsoleSize(Coord dimensions)
        {
            SetConsoleSize(dimensions.X, dimensions.Y);
        }

        public static void SetConsoleSize(short X = 80, short Y = 25)
        {
            if (X < Console.LargestWindowWidth - 5)
                Console.WindowWidth = X;
            else
                Console.WindowWidth = Console.LargestWindowWidth - 5;

            if (Y < Console.LargestWindowHeight - 5)
                Console.WindowHeight = Y;
            else
                Console.WindowHeight = Console.LargestWindowHeight - 5;


            if (Console.BufferWidth != X && Console.BufferWidth >= Console.WindowWidth)
                Console.BufferWidth = X;

            if (Console.BufferHeight != Y && Console.BufferHeight >= Console.WindowHeight)
                Console.BufferHeight = Y;

            lastDimesions = new Coord(X, Y);
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static void SetConsolePosition(int x, int y)
        {
            IntPtr ptr = GetConsoleWindow();
            MoveWindow(ptr, 0, 0, 80 * 8, 25 * 12, true);

            SetFontSize(LastFontSize);
            SetConsoleSize(lastDimesions);
        }

        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        public static void SetFixedConsole()
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);

        }

    }
}
