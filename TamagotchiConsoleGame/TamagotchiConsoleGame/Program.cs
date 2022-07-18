using TamagotchiConsoleGame.Tamagotchi;
using TamagotchiConsoleGame.Tamagotchi.Activities;
using TamagotchiConsoleGame.UI;

namespace TamagotchiConsoleGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game.Init();

            Activity main = new MainActivity();
            main.Execute(null);
        }
    }
}

