using System.Drawing;
using System.Windows.Forms;
using TamagotchiGraphicalGame;
using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine
{
    static class ConsoleManager
    {
        public static Coord LastFontSize { get; private set; }
        private static Coord lastDimesions;


        static ConsoleManager()
        {
            Program.MainWindow.Size = new Size(8 * 80 + 125, 12 * 25 + 125);

            LastFontSize = new Coord(8, 12);
            lastDimesions = new Coord(80, 25);
        }


        public static void SetFontSize(Coord dimensions)
        {
            SetFontSize(dimensions.X, dimensions.Y);
        }


        public static void SetFontSize(short X = 8, short Y = 12)
        {
            int x = lastDimesions.X * X + 125, y = lastDimesions.Y * Y + 125;

            SetGraphicalWindowSize(x, y);

            LastFontSize = new Coord(X, Y);
        }

        public static void SetConsoleSize(Coord dimensions)
        {
            SetConsoleSize(dimensions.X, dimensions.Y);
        }

        public static void SetConsoleSize(short X = 80, short Y = 25)
        {
            int x = LastFontSize.X * X + 125, y = LastFontSize.Y * Y + 125;

            SetGraphicalWindowSize(x, y);

            lastDimesions = new Coord(X, Y);
        }

        private static void SetGraphicalWindowSize(int X, int Y)
        {
            Program.MainWindow.Size = new Size(X, Y);

            int xPos = (Screen.PrimaryScreen.WorkingArea.Width - X) / 2, yPos = (Screen.PrimaryScreen.WorkingArea.Height - Y) / 2;
            if (xPos < 0 || yPos < 0)
                Program.MainWindow.Location = new Point(0, 0);
            else
                Program.MainWindow.Location = new Point(xPos, yPos);
        }
    }
}
