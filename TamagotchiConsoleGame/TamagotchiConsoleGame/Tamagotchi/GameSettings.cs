using System;

namespace TamagotchiConsoleGame.Tamagotchi
{
    [Serializable]
    public class GameSettings
    {
        private static readonly Lazy<GameSettings> lazy = new Lazy<GameSettings>(() => new GameSettings());

        public static GameSettings Instance { get { return lazy.Value; } }


        public int FontSize { get; set; }
        public int DemoMode { get; set; }

        private GameSettings()
        {
            FontSize = 0;
            DemoMode = 0;
        }

        private GameSettings(GameSettings settings)
        {
            this.FontSize = settings.FontSize;
            this.DemoMode = settings.DemoMode;
        }
    }
}
