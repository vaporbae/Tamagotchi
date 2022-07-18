using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.UI
{
    static class SpriteFactory
    {
        public static Sprite NewSprite(short X, short Y, string spriteName, string[] active, string inactive, int frameDuration = 200)
        {
            return NewSpriteC(new Coord(X, Y), spriteName, active, inactive, frameDuration);
        }


        public static Sprite NewSpriteC(Coord position, string spriteName, string[] active, string inactive, int frameDuration = 200)
        {

            Sprite sprite = new Sprite(position, frameDuration);
            SpriteBitmap bmp;

            foreach (string s in active)
            {
                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(s));
                sprite.SpriteSequence.Add(bmp);
            }

            if(inactive != null)
                sprite.SpriteSequence.Add(new SpriteBitmap(TGE.Instance.Resources.Get(inactive), false));

            TGE.Instance.SView.Add(spriteName, sprite);

            return sprite;
        }
    }
}
