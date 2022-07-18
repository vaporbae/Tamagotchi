using System;
using System.Collections.Generic;
using System.Drawing;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.UI
{
    public sealed class Menu : IUIControl
    {
        private List<Button> slots = new List<Button>();

        public Sprite SelectedIndicator;

        private Coord position;

        public Coord Position
        {
            get { return position; }
            set
            {
                position = value;

                // slow but I don't think we will use this method; if yes, needs to be rewritte
                foreach (Button mi in slots)
                    mi.Sprite.Position = position + mi.Sprite.Position;
            }
        }

        private bool active;

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;

                Sprite tmp;

                if (active == false)
                {
                    tmp = slots[currentMenu].Sprite;

                    tmp.AnimationEnabled = false;
                    tmp.AnimationFrame = tmp.SpriteSequence.Count - 1;
                }
                else
                {
                    if (selectedMenu != null)
                        currentMenu = active ? (int)selectedMenu : 0;
                    else
                        currentMenu = 0;

                    tmp = slots[currentMenu].Sprite;

                    tmp.AnimationEnabled = active;
                    tmp.AnimationFrame = active ? 0 : tmp.SpriteSequence.Count - 1;
                }

            }
        }

        private int currentMenu = 0;

        public int CurrrentMenu
        {
            get { return currentMenu; }
            set
            {
                currentMenu = value;

                Sprite tmp;

                foreach (Button s in slots)
                {
                    tmp = s.Sprite;
                    tmp.AnimationEnabled = false;
                    tmp.AnimationFrame = tmp.SpriteSequence.Count - 1;
                }


                tmp = slots[currentMenu].Sprite;
                tmp.AnimationEnabled = true;
                tmp.AnimationFrame = 0;
            }
        }

        private int? selectedMenu = null;

        public int? SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                selectedMenu = value;

                if (SelectedIndicator != null)
                {
                    if (selectedMenu == null)
                        SelectedIndicator.IsActive = false;
                    else
                    {
                        SelectedIndicator.IsActive = true;

                        Coord tmp = slots[(int)selectedMenu].Sprite.Position;

                        Bitmap tmp2 = SelectedIndicator.SpriteSequence[0].Bitmap;

                        SelectedIndicator.Position.X = (short)(tmp.X + slots[(int)selectedMenu].Sprite.SpriteSequence[0].Bitmap.Width / 2 - tmp2.Width / 2);
                        SelectedIndicator.Position.Y = (short)(tmp.Y - tmp2.Height / 2);

                    }
                }
            }
        }

        public Menu(Coord position)
        {
            this.position = new Coord(position);
        }

        public Menu(short X, short Y)
        {
            this.position = new Coord(X, Y);
        }

        public void AddItem(short X, short Y, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            AddItem(new Coord(X, Y), sprite, callback, active);
        }

        public void AddItem(Coord relativePosition, Sprite sprite, Events.OnClick callback, bool active = false)
        {
            Button tmp = new Button(relativePosition, sprite, callback);
            AddItem(tmp, active);
        }

        public void AddItem(Button button, bool active = false)
        {
            Sprite tmp = button.Sprite;

            tmp.Position = position + tmp.Position;

            slots.Add(button);

            if (active)
                CurrrentMenu = slots.Count - 1;
        }

        public void SetItemPosition(Coord relativePosition, Sprite sprite)
        {
            // slow but I don't think we will use this method; if yes, needs to be rewritten
            foreach (Button mi in slots)
            {
                if (mi.Sprite == sprite)
                {
                    mi.Sprite.Position = position + relativePosition;
                    mi.Sprite.Position = relativePosition;
                }
            }
        }

        public void KeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (currentMenu > 0)
                        changeCurrrentMenu(false);

                    break;

                case ConsoleKey.RightArrow:
                    if (currentMenu < slots.Count - 1)
                        changeCurrrentMenu(true);

                    break;

                case ConsoleKey.Enter:
                    if (SelectedIndicator != null)
                        SelectedMenu = currentMenu;

                    slots[currentMenu].KeyPressed(key);

                    break;

                default:
                    break;
            }
        }

        private void changeCurrrentMenu(bool increment)
        {
            int val = increment == true ? 1 : -1;

            Sprite tmp = slots[currentMenu].Sprite;
            tmp.AnimationEnabled = false;
            tmp.AnimationFrame = tmp.SpriteSequence.Count - 1;

            currentMenu += val;

            tmp = slots[currentMenu].Sprite;

            tmp.AnimationEnabled = true;
            tmp.AnimationFrame = 0;
        }

    }


}
