using System;
using System.Drawing;
using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine.Controls
{
    public enum TextAlignment { Left, Center, Right };

    public class Text : OnScreenControl
    {
        protected String content;

        public TextAlignment Align { get; set; } = TextAlignment.Left;

        public String Content
        {
            get { return content; }
            set
            {
                content = value;

                short width = 0;

                foreach (char character in Content)
                {
                    Bitmap characterBitmap = Misc.Font.Instance.GetBitmap(character);

                    width += (short)(characterBitmap.Width + 1);
                }

                Width = --width;
            }
        }

        public short Width { get; protected set; }

        public Color Foreground { get; set; }

        public Text() : this(new Coord(0, 0))
        {
            Foreground = Color.Black;
        }

        public Text(Coord Position)
        {
            Position = new Coord(Position);
            Foreground = Color.Black;
        }

        public Text(short X, short Y)
        {
            Position = new Coord(X, Y);
            Foreground = Color.Black;
        }

        public Text(String text) : this()
        {
            Content = text;
        }

        public Text(String text, Color foreground) : this()
        {
            Content = text;
            Foreground = foreground;
        }

        public Text(Coord position, String text) : this(position)
        {
            Content = text;
        }

        public Text(Coord position, String text, Color foreground) : this(position)
        {
            Content = text;
            Foreground = foreground;
        }

        public Text(short X, short Y, String text) : this(X, Y)
        {
            Content = text;
        }

        public Text(short X, short Y, String text, Color foreground) : this(X, Y)
        {
            Content = text;
            Foreground = foreground;
        }

        // add fucniton to draw with custom background and/or add rectangular drwa/fill function
        override public void Draw(ScreenBuffer buffer)
        {
            if (IsActive)
            {
                Coord tempPosition = new Coord(Position);

                if (Align == TextAlignment.Right)
                    tempPosition.X -= Width;
                else if (Align == TextAlignment.Center)
                    tempPosition.X -= (short)(Width / 2);

                foreach (char character in Content)
                {
                    Bitmap characterBitmap = Misc.Font.Instance.GetBitmap(character);

                    BitmapDraw.DrawOnBuffer(buffer, tempPosition, characterBitmap, Foreground);

                    tempPosition.X += (short)(characterBitmap.Width + 1);
                }
            }

        }
    }
}
