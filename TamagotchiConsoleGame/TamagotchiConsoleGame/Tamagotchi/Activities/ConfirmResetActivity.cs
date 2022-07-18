using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class ConfirmResetActivity : Activity
    {
        Serializer serializer = new Serializer();

        private Menu optionMenu;

        private void confirmReset()
        {
            serializer.Delete();
            Game.Tamagotchi = null;
            Exit();
        }


        protected override void onCreate(Activity sender)
        {
            Sprite a = SpriteFactory.NewSprite(0, 0, GameSprites.main_menu_bg, new string[] { GameResources.main_menu_bg }, null);

            a = SpriteFactory.NewSprite(0, 0, GameSprites.clouds, new string[] { GameResources.clouds }, null);

            for (int i = 0; i < 224; ++i)
                a.MotionSteps.Add(new Coord(-1, 0));
            a.MotionSteps.Add(new Coord(224, 0));

            a.MotionStepDuration = 40;
            a.MotionAnimationEnabled = true;

            Text b = new Text(112, 26, "Do you really want to", Color.DarkRed);
            b.Align = TextAlignment.Center;
            TGE.Instance.SView.Add("text_warning_line0", b);


            b = new Text(112, 29, "deleted game progress?", Color.DarkRed);
            b.Align = TextAlignment.Center;

            TGE.Instance.SView.Add("text_warning_line1", b);


            optionMenu = new Menu(85, 35);

            optionMenu.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.game_start_confirm,
                                                         new string[] { GameResources.game_start_confirm0, GameResources.game_start_confirm1 },
                                                         GameResources.game_start_confirm1, 350, confirmReset));

            optionMenu.AddItem(ButtonFactory.NewButton(30, 0, GameSprites.game_start_exit,
                                                         new string[] { GameResources.game_start_exit0, GameResources.game_start_exit1 },
                                                         GameResources.game_start_exit1, 350, Exit, true));
            optionMenu.CurrrentMenu = 1;
        }

        protected override void inLoop()
        {

        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            optionMenu.KeyPressed(cKey);
        }

        protected override void onExit()
        {

        }
    }
}
