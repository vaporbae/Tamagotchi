using System;
using System.Collections.Generic;
using System.Drawing;

namespace TextGraphicsEngine
{
    public sealed class Cell : IEquatable<Cell>, IClearable
    {
        private Color _foreground;
        private Color _background;

        private byte _character;

        public Cell()
        {
            Character = (byte)' ';
            Foreground = Color.Black;
            Background = Color.Black;
        }

        public bool IsModified { get; private set; }

        public void ClearModified()
        {
            IsModified = false;
        }

        public Color Foreground
        {
            get { return _foreground; }
            set
            {
                if (_foreground != value)
                {
                    _foreground = value;
                    IsModified = true;
                }

            }
        }

        public Color Background
        {
            get { return _background; }
            set
            {
                if (_background != value)
                {
                    _background = value;
                    IsModified = true;
                }

            }
        }

        public byte Character
        {
            get { return _character; }
            set
            {
                if (_character != value)
                {
                    _character = value;
                    IsModified = true;
                }
            }
        }

        public static bool operator ==(Cell obj1, Cell obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
                return false;

            return (obj1.Foreground == obj2.Foreground &&
                    obj1.Background == obj2.Background &&
                    obj1.Character == obj2.Character);
        }

        public static bool operator !=(Cell obj1, Cell obj2) => !(obj1 == obj2);

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public bool Equals(Cell other)
        {
            return other != null &&
                   EqualityComparer<Color>.Default.Equals(_foreground, other._foreground) &&
                   EqualityComparer<Color>.Default.Equals(_background, other._background) &&
                   _character == other._character &&
                   IsModified == other.IsModified &&
                   EqualityComparer<Color>.Default.Equals(Foreground, other.Foreground) &&
                   EqualityComparer<Color>.Default.Equals(Background, other.Background) &&
                   Character == other.Character;
        }

        public override int GetHashCode()
        {
            var hashCode = -393348255;
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(_foreground);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(_background);
            hashCode = hashCode * -1521134295 + _character.GetHashCode();
            hashCode = hashCode * -1521134295 + IsModified.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Foreground);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Background);
            hashCode = hashCode * -1521134295 + Character.GetHashCode();
            return hashCode;
        }

        public void Clear()
        {
            Character = (byte)' ';
            Background = Color.Black;
            Foreground = Color.Black;
        }
    }
}
