using System;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.UI
{

    // TODO !!!!!!!!!!!!!
    public sealed class Textbox : IUIControl
    {
        public Sprite Sprite = new Sprite();

        private Events.OnClick _onClick;

        private bool active;

        public bool Active
        {
            get { return active;  }
            set
            {
                active = value;

                if (active)
                {
                    Sprite.AnimationFrame = 0;
                    Sprite.AnimationEnabled = true;
                }
                else
                {
                    Sprite.AnimationFrame = Sprite.SpriteSequence.Count - 1;
                    Sprite.AnimationEnabled = false;
                }
            }
        }

        public Textbox(short X, short Y, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            SetItem(X, Y, sprite, callback, active);
        }

        public Textbox(Coord position, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            SetItem(position, sprite, callback, active);
        }

        public void SetItem(short X, short Y, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            SetItem(new Coord(X, Y), sprite, callback, active);
        }

        public void SetItem(Coord position, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            Sprite = sprite;
            sprite.Position = new Coord(position);
            _onClick = callback;
            Active = active;
        }

        public void KeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    _onClick();
                    break;

                default:
                    break;
            }
        }

    }
}
