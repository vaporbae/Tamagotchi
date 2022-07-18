using System.Drawing;

namespace TextGraphicsEngine.Misc
{
    public class SpriteBitmap
    {
        public Bitmap Bitmap { set; get; }

        public Coord PositionOffset;

        public bool IsAvaiable = true;

        public SpriteBitmap()
        {
            PositionOffset = new Coord(0, 0);
        }

        public SpriteBitmap(bool avaiable) : this()
        {
            IsAvaiable = avaiable;
        }

        public SpriteBitmap(Bitmap bitmap) : this()
        {
            Bitmap = bitmap;
        }

        public SpriteBitmap(Bitmap bitmap, bool avaiable) : this(avaiable)
        {
            Bitmap = bitmap;
        }

        public SpriteBitmap(short XOffset, short YOffset)
        {
            PositionOffset = new Coord(XOffset, YOffset);
        }

        public SpriteBitmap(short XOffset, short YOffset, bool avaiable) : this(XOffset, YOffset)
        {
            IsAvaiable = avaiable;
        }

        public SpriteBitmap(short XOffset, short YOffset, Bitmap bitmap) : this(XOffset, YOffset)
        {
            Bitmap = bitmap;
        }

        public SpriteBitmap(short XOffset, short YOffset, Bitmap bitmap, bool avaiable) : this(XOffset, YOffset, avaiable)
        {
            Bitmap = bitmap;
        }

        public SpriteBitmap(Coord offset)
        {
            PositionOffset = offset;
        }

        public SpriteBitmap(Coord offset, bool avaiable) : this(offset)
        {
            IsAvaiable = avaiable;
        }


        public SpriteBitmap(Coord offset, Bitmap bitmap) : this(offset)
        {
            Bitmap = bitmap;
        }

        public SpriteBitmap(Coord offset, Bitmap bitmap, bool avaiable) : this(offset, avaiable)
        {
            Bitmap = bitmap;
        }

        public void ReplaceColor(Color oldColor, Color newColor)
        {
            Bitmap = BitmapManipulation.ReplaceColor(Bitmap, oldColor, newColor);
        }

        public void Draw(ScreenBuffer buffer, Coord position)
        {
            position.X += PositionOffset.X;
            position.Y += PositionOffset.Y;

            BitmapDraw.DrawOnBuffer(buffer, position, Bitmap);

        }
    }
}
