using System;
using System.Collections.Generic;
using System.Drawing;
using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine
{
    public sealed class BitmapContainer
    {
        private Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();

        private string bitmapFolder;

        public string BitmapFolder
        {
            get { return bitmapFolder; }
            set
            {
                bitmapFolder = value;

                if (value.EndsWith("\\") == false)
                    BitmapFolder += "\\";
            }
        }


        public BitmapContainer()
        {
            BitmapFolder = "bitmaps\\";
        }
        public BitmapContainer(string folderName)
        {
            BitmapFolder = folderName;
        }

        private readonly string pathToBitmapFolderRoot = AppDomain.CurrentDomain.BaseDirectory;

        public Bitmap Add(string key, string filePath)
        {
            String path = pathToBitmapFolderRoot + BitmapFolder + filePath;
            Bitmap bmp = new Bitmap(path, false);

            bitmaps.Add(key, bmp);

            return bmp;
        }

        public Bitmap Add(string key, Tuple<string, int> pathAndScaleFactor)
        {
            String path = pathToBitmapFolderRoot + BitmapFolder + pathAndScaleFactor.Item1;
            Bitmap bmp = new Bitmap(path, false);

            int scaleFactor = pathAndScaleFactor.Item2;

            if (scaleFactor <= 0)
                scaleFactor = 1;

            Bitmap bmpScaled = BitmapManipulation.ResizeBitmap(bmp, bmp.Width * scaleFactor, bmp.Height * scaleFactor);

            bitmaps.Add(key, bmpScaled);

            return bmpScaled;
        }




        public Bitmap Get(string key)
        {
            return bitmaps[key];
        }


    }
}
