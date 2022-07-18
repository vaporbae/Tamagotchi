using System.Drawing;
using System.Drawing.Imaging;
using TamagotchiGraphicalGame;
using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine
{
    public sealed class ScreenBuffer : Buffer
    {
        public ScreenBuffer() : base()
        {
        }

        public ScreenBuffer(short width, short heigth) : base(width, heigth)
        {
        }

        private Bitmap lastResultImage = null;

        public unsafe void Draw(bool forceRefresh = false)
        {

            short height = (short)(Height * 2), width = Width;
            short skipY = 2 * 2, skipX = 1;

            Bitmap resultImage;
            if (lastResultImage != null)
                resultImage = lastResultImage;
            else
                resultImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            for (short y = 0; y < Height; ++y)
                for (short x = 0; x < width; ++x)
                {
                    if (this[(short)(x / skipX), (short)(y)].IsModified || forceRefresh)
                    {
                        int i;

                        Color c = this[(short)(x / skipX), (short)(y)].Background;

                        for (i = 0; i < skipY / 4; ++i)
                            resultImage.SetPixel(x, y * skipY / 2 + i, c);

                        c = this[(short)(x / skipX), (short)(y)].Foreground;

                        for (; i < skipY / 2; ++i)
                            resultImage.SetPixel(x, y * skipY / 2 + i, c);

                        this[(short)(x / skipX), (short)(y)].ClearModified();
                    }
                }



            lastResultImage = resultImage;

            Program.Screen.Image = BitmapManipulation.ResizeBitmap(resultImage, width * ConsoleManager.LastFontSize.X, Height * ConsoleManager.LastFontSize.Y);
        }


        public Bitmap GetBitmap(bool realMode)
        {
            short height = (short)(Height), width = Width;
            short skipY = 2, skipX = 1;

            if (realMode == true)
            {
                height *= ConsoleManager.LastFontSize.Y;
                width *= ConsoleManager.LastFontSize.X;

                skipY *= ConsoleManager.LastFontSize.Y;
                skipX *= ConsoleManager.LastFontSize.X;
            }
            else
            {
                height *= 2;
                skipY *= 2;
            }

            Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            for (short y = 0; y < Height; ++y)
                for (short x = 0; x < width; ++x)
                {
                    int i;
                    for (i = 0; i < skipY / 4; ++i)
                        resultImage.SetPixel(x, y * skipY / 2 + i, this[(short)(x / skipX), (short)(y)].Background);

                    for (; i < skipY / 2; ++i)
                        resultImage.SetPixel(x, y * skipY / 2 + i, this[(short)(x / skipX), (short)(y)].Foreground);
                }

            return resultImage;
        }

    }
}
