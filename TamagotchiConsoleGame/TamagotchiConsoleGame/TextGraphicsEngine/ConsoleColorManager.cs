using Colorful;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace TextGraphicsEngine
{
    static class ConsoleColorManager
    {
        private static ColorMapper colorMaper = new ColorMapper();

        private static Map<Color, ConsoleColor> colorMap = new Map<Color, ConsoleColor>();

        private static ConsoleColor nextColorSlot = ConsoleColor.Black;

        public static List<Color> RequiredColors = new List<Color>();

        static ConsoleColorManager()
        {

        }

        public static void Reset()
        {
            colorMaper = new ColorMapper();
            colorMap = new Map<Color, ConsoleColor>();
            nextColorSlot = ConsoleColor.Black;
            RequiredColors = new List<Color>();
        }

        // there are some bugs when colors are replaced
        public static ConsoleColor GetConsoleColor(Color color)
        {
            if (colorMap.Forward.Contains(color))
                return colorMap.Forward[color];


            ConsoleColor colorSlot = nextColorSlot;

            if (colorMap.Count() < 16 || RequiredColors == null)
            {
                if (colorMap.Forward.Contains(color))
                {
                    return colorMap.Forward[color];
                }
                else
                {
                    colorMap.Add(color, colorSlot);
                    colorMaper.MapColor(colorSlot, color);

                    nextColorSlot = (ConsoleColor)(((int)colorSlot + 1) % 16);

                    return colorSlot;
                }
            }
            else
            {
                List<int> freeSlots = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

                foreach (Color c in RequiredColors)
                {
                    if (colorMap.Forward.Contains(c))
                        freeSlots.Remove((int)colorMap.Forward[c]);
                }

                if (freeSlots.Count > 0)
                {
                    // don't know why but wokrs better with usedSlots.Count - 1 than 0; or maybe not - there are some bugs when colors are replaced
                    colorSlot = (ConsoleColor)freeSlots[0];

                    colorMap.Remove(colorMap.Reverse[colorSlot], colorSlot);
                    colorMap.Add(color, colorSlot);
                    colorMaper.MapColor(colorSlot, color);
                }
                else
                    colorSlot = ConsoleColor.Black;
            }


            return colorSlot;
        }



    }
}
