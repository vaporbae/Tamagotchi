using System.Drawing;
using System.Drawing.Imaging;

namespace TextGraphicsEngine.Misc
{
    public static class BitmapDraw
    {
        public static void DrawOnBuffer(ScreenBuffer buffer, Coord position, Bitmap bitmap)
        {
            DrawOnBuffer(buffer, position, bitmap, false, Color.Black);
        }

        public static void DrawOnBuffer(ScreenBuffer buffer, Coord position, Bitmap bitmap, Color colorReplacement)
        {
            DrawOnBuffer(buffer, position, bitmap, true, colorReplacement);
        }


        private static unsafe void DrawOnBuffer(ScreenBuffer buffer, Coord position, Bitmap bitmap, bool colorOverride, Color colorReplacement)
        {
            const uint pixelSize = 4; // 32 bits per pixel

            BitmapData sourceBitmap = null;

            short yShift = position.Y, xShift = position.X;
            int width = bitmap.Width, height = bitmap.Height;

            try
            {
                sourceBitmap = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                short realY = 0;

                bool mode = true;

                for (int y = 0; y < height; ++y, mode = !mode)
                {
                    byte* sourceRow = (byte*)sourceBitmap.Scan0 + (y * sourceBitmap.Stride);
                    short currYShift = (short)(realY + yShift);

                    for (int x = 0; x < width; ++x)
                    {
                        short currXShift = (short)(x + xShift);

                        byte b = sourceRow[x * pixelSize + 0];
                        byte g = sourceRow[x * pixelSize + 1];
                        byte r = sourceRow[x * pixelSize + 2];
                        byte a = sourceRow[x * pixelSize + 3];

                        if (mode)
                        {
                            if (a != 0)
                            {

                                if (colorOverride == true)
                                {
                                    buffer[currXShift, currYShift].Background = colorReplacement;

                                    if (!ConsoleColorManager.RequiredColors.Contains(colorReplacement))
                                        ConsoleColorManager.RequiredColors.Add(colorReplacement);
                                }
                                else
                                {
                                    Color tmp = Color.FromArgb(0, r, g, b);

                                    buffer[currXShift, currYShift].Background = tmp;

                                    if (!ConsoleColorManager.RequiredColors.Contains(tmp))
                                        ConsoleColorManager.RequiredColors.Add(tmp);
                                }

                                buffer[currXShift, currYShift].Character = 0xDC;
                            }

                        }
                        else
                        {
                            if (a != 0)
                            {
                                if (colorOverride == true)
                                {
                                    buffer[currXShift, currYShift].Foreground = colorReplacement;

                                    if (!ConsoleColorManager.RequiredColors.Contains(colorReplacement))
                                        ConsoleColorManager.RequiredColors.Add(colorReplacement);
                                }
                                else
                                {
                                    Color tmp = Color.FromArgb(0, r, g, b);

                                    buffer[currXShift, currYShift].Foreground = tmp;

                                    if (!ConsoleColorManager.RequiredColors.Contains(tmp))
                                        ConsoleColorManager.RequiredColors.Add(tmp);
                                }

                                buffer[currXShift, currYShift].Character = 0xDC;
                            }

                        }

                    }

                    if (!mode)
                        ++realY;
                }
            }
            finally
            {
                if (sourceBitmap != null)
                    bitmap.UnlockBits(sourceBitmap);
            }








            //short yShift = position.Y, xShift = position.X;

            //for (short x = 0; x < bitmap.Width; ++x)
            //{
            //    short currXShift = (short)(x + xShift);
            //    short realY = 0;

            //    bool mode = true;

            //    for (short y = 0; y < bitmap.Height; ++y, mode = !mode)
            //    {
            //        short currYShift = (short)(realY + yShift);

            //        Color color = bitmap.GetPixel(x, y);
            //        if (mode)
            //        {
            //            if (color.A != 0)
            //            {
            //                if (colorOverride == true)
            //                    buffer[currXShift, currYShift].Background = colorReplacement;
            //                else
            //                    buffer[currXShift, currYShift].Background = Color.FromArgb(0, color);

            //            }

            //            buffer[currXShift, currYShift].Character = 0xDC;
            //        }
            //        else
            //        {
            //            if (color.A != 0)
            //            {
            //                if (colorOverride == true)
            //                    buffer[currXShift, currYShift].Foreground = colorReplacement;
            //                else
            //                    buffer[currXShift, currYShift].Foreground = Color.FromArgb(0, color);

            //            }

            //            ++realY;
            //        }
            //    }
            //}
        }
    }
}
