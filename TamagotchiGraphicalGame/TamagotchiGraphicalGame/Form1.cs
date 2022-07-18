using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

using TamagotchiConsoleGame.Tamagotchi;
using TamagotchiConsoleGame.Tamagotchi.Activities;
using TamagotchiConsoleGame.UI;

namespace TamagotchiGraphicalGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            Program.Screen = ScreenImage;
            ScreenImage.Image = new Bitmap(900, 480, PixelFormat.Format24bppRgb);
        }

        private Thread gameThread;

        private void Form1_Load(object sender, EventArgs e)
        {
            gameThread = new Thread(gameThreadMethod);
            gameThread.Start();
        }


        Activity main;
        public void gameThreadMethod()
        {
            Game.Init();

            main = new MainActivity();
            main.Execute(null);

            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(main != null)
                main.Exit();
            gameThread.Abort();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Program.Key = e.KeyCode;
        }
    }
}
