using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TamagotchiGraphicalGame
{
    public static class Program
    {
        public static Form MainWindow;
        public static PictureBox Screen;
        public static Keys? Key;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new Form1();
            Application.Run(MainWindow);
        }
    }
}
