using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiConsoleGame.UI
{
    interface IUIControl
    {
        bool Active { get; set; }
        void KeyPressed(ConsoleKey key);
    }
}
