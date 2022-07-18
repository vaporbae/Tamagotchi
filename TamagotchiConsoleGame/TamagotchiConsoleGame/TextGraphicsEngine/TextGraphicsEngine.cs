using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TextGraphicsEngine
{
    public sealed class TGE
    {
        private static readonly Lazy<TGE> lazy = new Lazy<TGE>(() => new TGE());

        public static TGE Instance { get { return lazy.Value; } }

        public ScreenBuffer SBuffer { get; set; }
        public View SView { get; set; }

        public BitmapContainer Resources { get; set; }

        private TGE()
        {
            SBuffer = new ScreenBuffer();
            SView = new View();
            Resources = new BitmapContainer("bitmaps");
        }

        public void Render(bool forceRefresh = false)
        {
            if (SBuffer != null)
            {
                if (SView != null)
                    SView.Render(SBuffer);

                SBuffer.Draw(forceRefresh);
            }
        }

        public void TakeScreenshot(bool realMode)
        {
            Bitmap bmp = SBuffer.GetBitmap(realMode);
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "screenshots/");
            string fileName = DateTime.Now.ToString("dd-MM-yyyy h-mm-ss.ffff tt") + ".png";
            string path = Path.Combine(folder, fileName); ;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            bmp.Save(path, ImageFormat.Png);

            bmp.Dispose();
        }

    }

}
