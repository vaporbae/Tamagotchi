using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class MainActivity : Activity
    {
        private Menu menu;

        private void enterOptions()
        {
            GoToActivity(new OptionsActivity());
        }

        private void enterGame()
        {
            GoToActivity(new StartGameActivity());
        }



        protected override void onCreate(Activity sender)
        {
            var a = SpriteFactory.NewSprite(0, 0, GameSprites.main_menu_bg, new string[] { GameResources.main_menu_bg }, null);

            //for (int i = 0; i < 224; ++i)
            //    a.MotionSteps.Add(new Coord(1, 0));
            //a.MotionSteps.Add(new Coord(-224, 0));

            //a.MotionStepDuration = 60;

            //a.MotionAnimationEnabled = true;


            a = SpriteFactory.NewSprite(0, 0, GameSprites.clouds, new string[] { GameResources.clouds }, null);

            for (int i = 0; i< 224; ++i)
                a.MotionSteps.Add(new Coord(-1, 0));
            a.MotionSteps.Add(new Coord(224, 0));

            a.MotionStepDuration = 40;
            a.MotionAnimationEnabled = true;


            var bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.logo));
            bmp.ReplaceColor(Color.FromArgb(0, 168, 0, 0), Color.Black);
            a = new Sprite(29, 4, bmp);
            TGE.Instance.SView.Add(GameSprites.logo_shadow, a);

            //a.MotionSteps.Add(new Coord(-2, -2));
            //a.MotionSteps.Add(new Coord(2, 2));
            //a.MotionAnimationEnabled = true;


            bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.logo));
            a = new Sprite(28, 3, bmp);
            TGE.Instance.SView.Add(GameSprites.logo, a);


            menu = new Menu(15, 38);

            menu.AddItem(ButtonFactory.NewButton(0, 12, GameSprites.options,
                                                        new string[] { GameResources.options0, GameResources.options1 },
                                                        GameResources.options2_inactive, 350, enterOptions));

            menu.AddItem(ButtonFactory.NewButton(70, 0, GameSprites.play,
                                                        new string[] { GameResources.play0, GameResources.play1 },
                                                        GameResources.play2_inactive, 350, enterGame), true);

            menu.AddItem(ButtonFactory.NewButton(141, 12, GameSprites.exit,
                                                        new string[] { GameResources.exit0, GameResources.exit1 },
                                                        GameResources.exit2_inactive, 350, Exit));

        }

        protected override void inLoop()
        {

        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            menu.KeyPressed(cKey);
        }

        protected override void onExit()
        {

        }

    }
}
