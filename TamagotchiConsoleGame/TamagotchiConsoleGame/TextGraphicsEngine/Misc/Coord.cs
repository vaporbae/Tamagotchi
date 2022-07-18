using System.Runtime.InteropServices;

namespace TextGraphicsEngine.Misc
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Coord(Coord coord)
        {
            this.X = coord.X;
            this.Y = coord.Y;
        }

        public static Coord operator +(Coord b, Coord c)
        {
            Coord coord = new Coord();
            coord.X = (short)(b.X + c.X);
            coord.Y = (short)(b.Y + c.Y);

            return coord;
        }
    };

}
