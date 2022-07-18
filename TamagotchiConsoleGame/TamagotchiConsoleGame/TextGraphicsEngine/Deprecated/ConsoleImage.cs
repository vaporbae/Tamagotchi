using System;
using System.Drawing;
using Console = Colorful.Console;

namespace TextGraphicsEngine.Deprecated
{
    public static class ConsoleImage
    {
        public static Color FromColor(ConsoleColor c)
        {
            int[] cColors = {   0x000000, //Black = 0
                                0x000080, //DarkBlue = 1
                                0x008000, //DarkGreen = 2
                                0x008080, //DarkCyan = 3
                                0x800000, //DarkRed = 4
                                0x800080, //DarkMagenta = 5
                                0x808000, //DarkYellow = 6
                                0xC0C0C0, //Gray = 7
                                0x808080, //DarkGray = 8
                                0x0000FF, //Blue = 9
                                0x00FF00, //Green = 10
                                0x00FFFF, //Cyan = 11
                                0xFF0000, //Red = 12
                                0xFF00FF, //Magenta = 13
                                0xFFFF00, //Yellow = 14
                                0xFFFFFF  //White = 15
                            };

            return Color.FromArgb(cColors[(int)c]);
        }
        public static ConsoleColor ToConsoleColor(Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
            index |= (c.R > 64) ? 4 : 0;
            index |= (c.G > 64) ? 2 : 0;
            index |= (c.B > 64) ? 1 : 0;

            return (ConsoleColor)index;
        }

        public static void WriteInDefaultColors(Bitmap bitmap)
        {
            Bitmap bmpMin = new Bitmap(bitmap);
            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; ++j)
                {
                    Console.ForegroundColor = FromColor((ConsoleColor)ToConsoleColor(bmpMin.GetPixel(j, i)));
                    Console.Write("██");
                }
                System.Console.WriteLine();
            }
        }

        public static void WriteInARGB(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Height - 1; i += 2)
            {
                for (int j = 0; j < bitmap.Width; ++j)
                {
                    Color lowerPart = bitmap.GetPixel(j, i + 1);
                    if (lowerPart.A != 0)
                        Console.ForegroundColor = Color.FromArgb(0, lowerPart);
                    else
                        Console.ForegroundColor = Color.Black;

                    Color upperPart = bitmap.GetPixel(j, i);
                    if (upperPart.A != 0)
                        Console.BackgroundColor = Color.FromArgb(0, upperPart);
                    else
                        Console.BackgroundColor = Color.Black;

                    Console.Write('▄');

                }
                Console.ForegroundColor = Color.Black;
                Console.BackgroundColor = Color.Black;


                Console.Write('\n');
            }
        }
    }
}
