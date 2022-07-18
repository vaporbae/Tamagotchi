using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using TextGraphicsEngine.Misc;

using Console = Colorful.Console;

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


        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
           string fileName,
           [MarshalAs(UnmanagedType.U4)] uint fileAccess,
           [MarshalAs(UnmanagedType.U4)] uint fileShare,
           IntPtr securityAttributes,
           [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
           [MarshalAs(UnmanagedType.U4)] int flags,
           IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
              SafeFileHandle hConsoleOutput,
              CharInfo[] lpBuffer,
              Coord dwBufferSize,
              Coord dwBufferCoord,
              ref SmallRect lpWriteRegion);


        [StructLayout(LayoutKind.Explicit)]
        private struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        private static SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        private struct ScreenOutputBuffer
        {
            public CharInfo[] Content;

            public int AvaiableSpace
            {
                get { return Content != null ? Content.Length : 0; }
                set
                {
                    if (Content != null)
                    {
                        // if current size of buffer is smaller than required, create new buffer
                        if (this.Content.Length < value)
                            this.Content = new CharInfo[value];
                    }
                    else this.Content = new CharInfo[value];
                }
            }
        };

        private ScreenOutputBuffer outputBuffer;


        [STAThread]
        public void Draw(bool forceRefresh = false)
        {
            if (!h.IsInvalid)
            {
                // insert required screen content buffer space
                outputBuffer.AvaiableSpace = Width * Height;

                //ConsoleColorManager.RequiredColors.Clear();

                for (short y = 0; y < Height; ++y)
                {
                    int shift = y * Width;

                    for (short x = 0; x < Width; ++x)
                    {

                        //Color fg = this[x, y].Foreground, bg = this[x, y].Background;

                        //if (!ConsoleColorManager.RequiredColors.Contains(fg))
                        //    ConsoleColorManager.RequiredColors.Add(fg);

                        //if (!ConsoleColorManager.RequiredColors.Contains(bg))
                        //    ConsoleColorManager.RequiredColors.Add(bg);

                        if (this[x, y].IsModified || forceRefresh)
                        {
                            Color fg = this[x, y].Foreground, bg = this[x, y].Background;

                            byte foregroundColor = (byte)ConsoleColorManager.GetConsoleColor(fg);
                            byte backgroundColor = (byte)ConsoleColorManager.GetConsoleColor(bg);

                            outputBuffer.Content[x + shift].Attributes = (short)(foregroundColor | (backgroundColor << 4));
                            outputBuffer.Content[x + shift].Char.AsciiChar = (byte)this[x, y].Character;

                            this[x, y].ClearModified();
                        }
                    }
                }

                SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = Width, Bottom = Height };

                bool b = WriteConsoleOutput(h, outputBuffer.Content,
                                            new Coord() { X = Width, Y = Height },
                                            new Coord() { X = 0, Y = 0 },
                                            ref rect);
            }
        }

        public void DrawSlow()
        {
            bool entryCursorVisibility = Console.CursorVisible;
            Console.CursorVisible = false;

            for (short y = 0; y < Height; ++y)
                for (short x = 0; x < Width; ++x)
                    if (this[x, y].IsModified)
                    {
                        Console.CursorLeft = x;
                        Console.CursorTop = y;

                        Console.BackgroundColor = this[x, y].Background;
                        Console.ForegroundColor = this[x, y].Foreground;

                        Console.Write(this[x, y].Character);

                        this[x, y].ClearModified();
                    }

            Console.CursorVisible = entryCursorVisibility;
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
