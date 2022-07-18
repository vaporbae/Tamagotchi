using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.UI
{
    static class ButtonFactory
    {
        public static Button NewButton(short X, short Y, string spriteName, string[] active, string inactive, int frameDuration, Events.OnClick callback, bool buttonActive = false)
        {
            return NewButtonC(new Coord(X, Y), spriteName, active, inactive, frameDuration, callback, buttonActive);
        }

        public static Button NewButtonC(Coord position, string spriteName, string[] active, string inactive, int frameDuration, Events.OnClick callback, bool buttonActive = false)
        {

            Sprite sprite = SpriteFactory.NewSpriteC(position, spriteName, active, inactive, frameDuration);

            return new Button(position, sprite, callback, buttonActive);
        }
    }
}
