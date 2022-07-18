using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using TextGraphicsEngine;

namespace TamagotchiConsoleGame.Tamagotchi
{
    public static class Game
    {
        public static Character Tamagotchi;
        private static System.Timers.Timer mainGameTimer;

        private static int softwareInitTimer = 0;

        private static string[] loading =   { "                                                       ",
                                              "  x     xxxxxx xxxxx xxx    x x    x xxxxxx            ",
                                              "  x     x    x x   x x  x   x xx   x x                 ",
                                              "  x     x    x x   x x   x  x x x  x x                 ",
                                              "  x     x    x xxxxx x    x x x  x x x  xxx            ",
                                              "  x     x    x x   x x   x  x x   xx x    x            ",
                                              "  x     x    x x   x x  x   x x    x x    x            ",
                                              "  xxxxx xxxxxx x   x xxx    x x    x xxxxxx xx xx xx   ",
                                              "                                                       " };



        public static void Init()
        {
            //initConsole();

            initTimer();

            ScreenBuffer tmpBuffer = new ScreenBuffer(80, 25);
            displayLoadingSplashScreen(tmpBuffer);

            tmpBuffer.Clear();
            tmpBuffer.Draw();

            //ConsoleManager.SetFontSize(4, 6);
            //ConsoleManager.SetConsoleSize(225, 80);

            Serializer serializer = new Serializer();

            if (serializer.SettingsExists())
            {
                GameSettings settings = serializer.DeserializeSettings();

                GameSettings.Instance.FontSize = settings.FontSize;
                GameSettings.Instance.DemoMode = settings.DemoMode;
            }
            setConsole();
        }

        private static void setConsole()
        {
            switch (GameSettings.Instance.FontSize)
            {
                case 0:
                    font0();
                    break;
                case 1:
                    font1();
                    break;
                case 2:
                    font2();
                    break;
                case 3:
                    font3();
                    break;
                case 4:
                    font4();
                    break;
            }
        }

        public static void font0()
        {
            ConsoleManager.SetFontSize(4, 6);
            ConsoleManager.SetConsoleSize(225, 80);

            GameSettings.Instance.FontSize = 0;
        }

        public static void font1()
        {
            ConsoleManager.SetFontSize(6, 8);
            ConsoleManager.SetConsoleSize(225, 80);

            GameSettings.Instance.FontSize = 1;
        }


        public static void font2()
        {
            ConsoleManager.SetFontSize(8, 12);
            ConsoleManager.SetConsoleSize(225, 80);

            GameSettings.Instance.FontSize = 2;
        }

        public static void font3()
        {
            ConsoleManager.SetFontSize(12, 16);
            ConsoleManager.SetConsoleSize(225, 80);

            GameSettings.Instance.FontSize = 3;
        }

        public static void font4()
        {
            ConsoleManager.SetFontSize(10, 18);
            ConsoleManager.SetConsoleSize(225, 80);

            GameSettings.Instance.FontSize = 4;
        }

        private static void displayLoadingSplashScreen(ScreenBuffer tmpBuffer)
        {
            Random rnd = new Random();

            short animFrameCount = 0, maxCount = 25;
            bool[] stepLoader = new bool[maxCount];

            int resourceLoaded = 0;
            List<string> keyList = null;
            int oneStepPartialLoadAmout = GameResources.List.Count / 20;

            int multiplier = 1, loaderStep = 3;

            mainGameTimer.Enabled = true;
            while (animFrameCount != maxCount)
            {
                if (softwareInitTimer == 0)
                {
                    softwareInitTimer = rnd.Next(8, 12);
                    stepLoader[animFrameCount] = true;
                    ++animFrameCount;

                    for (short y = 0; y < loading.Length; ++y)
                        for (short x = 0; x < loading[y].Length; ++x)
                        {
                            short shiftX = (animFrameCount % 2 == 0) ? (short)0 : (short)2;
                            short shiftY = (animFrameCount % 4 == 0) ? (short)0 : (short)1;

                            short realX = (short)(x + 23 + shiftX), realY = (short)(y + 14 + shiftY);

                            if ((byte)loading[y][x] == 'x')
                            {
                                tmpBuffer[realX, realY].Foreground = Color.Green;
                                tmpBuffer[realX, realY].Background = Color.GreenYellow;

                            }
                            else
                            {
                                tmpBuffer[realX, realY].Foreground = Color.Black;
                                tmpBuffer[realX, realY].Background = Color.Black;
                            }
                        }

                    tmpBuffer.Draw();
                }

                // RESOURCE LOADING

                // after loading text was written for the first time do nothing to ensure loding was animated and then load resources
                if (stepLoader[0])
                {
                    stepLoader[0] = false;

                }

                else if (stepLoader[1])
                {
                    stepLoader[1] = false;

                    TGE.Instance.SBuffer = new ScreenBuffer(225, 80);
                }

                else if (stepLoader[2])
                {
                    stepLoader[2] = false;

                    keyList = new List<string>(GameResources.List.Keys);

                    //foreach (var item in GameResources.List)
                    //{
                    //    TGE.Instance.Resources.Add(item.Key, item.Value);
                    //}
                }

                else if (stepLoader[loaderStep])
                {
                    stepLoader[loaderStep] = false;

                    resourceLoaded = loadResources(keyList, resourceLoaded, oneStepPartialLoadAmout * multiplier);

                    if (++loaderStep > 21)
                        loaderStep = 21;

                    if (++multiplier > 19)
                        loaderStep = 19;
                }


                else if (stepLoader[22])
                {
                    stepLoader[22] = false;

                    resourceLoaded = loadResources(keyList, resourceLoaded, GameResources.List.Count);
                }

                else if (stepLoader[23])
                {
                    stepLoader[23] = false;

                    foreach (var item in GameResources.ScaledList)
                    {
                        TGE.Instance.Resources.Add(item.Key, item.Value);
                    }
                }

                else if (stepLoader[24])
                {
                    stepLoader[24] = false;

                    loadFont();
                }
            }

            mainGameTimer.Enabled = false;

        }

        private static int loadResources(List<string> keys, int start, int end)
        {
            int i;
            for (i = start; i < end; ++i)
            {
                string key = keys[i], path;

                if (GameResources.List.TryGetValue(key, out path))
                {
                    TGE.Instance.Resources.Add(key, path);
                }
            }
            return i;
        }

        //private static void initConsole()
        //{
        //    Console.Title = "Tamagotchi";
        //    ConsoleManager.SetFixedConsole();
        //    ConsoleManager.SetConsolePosition(0, 0);
        //    ConsoleManager.SetFontSize();
        //    ConsoleManager.SetConsoleSize();

        //    Console.BackgroundColor = Color.Black;
        //    Console.ForegroundColor = Color.White;
        //    Console.CursorVisible = false;
        //    Console.Clear();
        //}

        private static void initTimer()
        {
            mainGameTimer = new System.Timers.Timer();
            mainGameTimer.Elapsed += new System.Timers.ElapsedEventHandler(mainTimerTick);
            mainGameTimer.Interval = 10;
            mainGameTimer.Enabled = false;
        }

        [MethodImplAttribute(MethodImplOptions.NoOptimization)]
        private static void loadFont() => TextGraphicsEngine.Misc.Font.Instance.GetBitmap('L');

        // Specify what you want to happen when the Elapsed event is raised.
        private static void mainTimerTick(object source, System.Timers.ElapsedEventArgs e)
        {
            if (softwareInitTimer != 0)
                --softwareInitTimer;
        }
    }
}

