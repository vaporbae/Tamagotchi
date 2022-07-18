using System;
using System.Collections.Generic;

namespace TamagotchiConsoleGame.UI
{
    class MultiLevelMenu : IUIControl
    {
        public List<IUIControl> Items = new List<IUIControl>();

        public bool Active
        {
            get
            {
                return Items[activeLevel].Active;
            }
            set
            {
            }
        }

        public bool KeepSelected = false;

        private int activeLevel = 0;

        public int ActiveLevel
        {
            get { return activeLevel; }
            set
            {
                Items[activeLevel].Active = false;
                activeLevel = value;
                Items[activeLevel].Active = true;
            }
        }

        public void Add(IUIControl control)
        {
            Items.Add(control);

            if (Items.Count == 1)
                control.Active = true;
            else
                control.Active = false;
        }

        public void KeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (ActiveLevel > 0)
                    {
                        selectionKeeper(false);

                        --ActiveLevel;
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (ActiveLevel < Items.Count - 1)
                    {
                        selectionKeeper(true);

                        ++ActiveLevel;
                    }

                    break;

                default:
                    Items[activeLevel].KeyPressed(key);
                    break;
            }
        }


        private void selectionKeeper(bool increment)
        {
            int val = increment == true ? 1 : -1;

            if (KeepSelected == true)
            {
                IUIControl current = Items[ActiveLevel], next = Items[ActiveLevel + val];

                if (current is Menu && next is Menu)
                {
                    Menu currentMenu = (Menu)current, nextMenu = (Menu)next;

                    nextMenu.SelectedMenu = currentMenu.CurrrentMenu;
                }

            }
        }
    }
}
