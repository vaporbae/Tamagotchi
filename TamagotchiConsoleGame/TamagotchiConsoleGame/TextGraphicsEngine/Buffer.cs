using System.Collections.Generic;
using System.Drawing;

namespace TextGraphicsEngine
{
    public class Buffer : IClearable
    {
        protected List<List<Cell>> bufferContent = new List<List<Cell>>();
        public short Width { get; private set; }
        public short Height { get; private set; }

        public Buffer() : this(80, 25)
        {

        }

        public Buffer(short width, short heigth)
        {
            SetSize(width, heigth);
        }

        public void SetSize(short width, short heigth)
        {
            if (heigth > Height)
                for (short y = Height; y < heigth; ++y)
                    bufferContent.Add(new List<Cell>());

            else if (heigth < Height)
                for (short y = heigth; y < Height; ++y)
                    bufferContent.RemoveAt(y);

            Height = heigth;

            if (width > Width)
                foreach (var sublist in bufferContent)
                    for (short x = Width; x < width; ++x)
                        sublist.Add(new Cell());

            else if (width < Width)
                foreach (var sublist in bufferContent)
                    for (short x = Width; x < width; ++x)
                        sublist.RemoveAt(x);

            Width = width;
        }

        public void Update(Buffer source)
        {
            var sourceBuffer = source.bufferContent;

            if (Width != source.Width || Height != source.Height)
                SetSize(source.Width, source.Height);

            for (short x = 0; x < bufferContent.Count; ++x)
                for (short y = 0; y < bufferContent[x].Count; ++y)
                {
                    this[x, y].Background = source[x, y].Background;
                    this[x, y].Foreground = source[x, y].Foreground;
                    this[x, y].Character = source[x, y].Character;
                }
        }

        public void Clear()
        {
            for (short y = 0; y < Height; ++y)
                for (short x = 0; x < Width; ++x)
                    this[x, y].Clear();
        }


        public Cell this[short x, short y]
        {
            get
            {
                if (x >= 0 && x < Width && y >= 0 && y < Height)
                    return bufferContent[y][x];

                return bufferContent[0][0];
            }
            set
            {
                if (x >= 0 && x < Width && y >= 0 && y < Height)
                    bufferContent[y][x] = value;
            }
        }


    }
}
