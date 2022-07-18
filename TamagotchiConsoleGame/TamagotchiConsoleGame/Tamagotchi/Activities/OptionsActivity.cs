using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class OptionsActivity : Activity
    {
        Serializer serializer = new Serializer();

        MultiLevelMenu optionsMenu;

        Button goBack, resetGame;


        private void demoOn()
        {
            GameSettings.Instance.DemoMode = 1;
        }


        private void demoOff()
        {
            GameSettings.Instance.DemoMode = 0;
        }


        private void resetGameFunc()
        {
            GoToActivity(new ConfirmResetActivity());
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

            Text b = new Text(4, 9, "Character size:", Color.Black);
            TGE.Instance.SView.Add("text_ch_size", b);

            // FONT SIZE MENU
            Menu fontSizeMenu = new Menu(72, 8);

            a = new Sprite(new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.options_indicator)));
            TGE.Instance.SView.Add(GameSprites.options_indicator + "_1", a);

            fontSizeMenu.SelectedIndicator = a;



            fontSizeMenu.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.options_4x6,
                                                         new string[] { GameResources.options_4x6_0, GameResources.options_4x6_1},
                                                         GameResources.options_4x6_2, 350, Game.font0));

            fontSizeMenu.AddItem(ButtonFactory.NewButton(17, 0, GameSprites.options_6x8,
                                                         new string[] { GameResources.options_6x8_0, GameResources.options_6x8_1},
                                                         GameResources.options_6x8_2, 350, Game.font1));

            fontSizeMenu.AddItem(ButtonFactory.NewButton(34, 0, GameSprites.options_8x12,
                                                         new string[] { GameResources.options_8x12_0, GameResources.options_8x12_1 },
                                                         GameResources.options_8x12_2, 350, Game.font2));

            fontSizeMenu.AddItem(ButtonFactory.NewButton(53, 0, GameSprites.options_10x18,
                                                         new string[] { GameResources.options_10x18_0, GameResources.options_10x18_1 },
                                                         GameResources.options_10x18_2, 350, Game.font3));

            fontSizeMenu.AddItem(ButtonFactory.NewButton(74, 0, GameSprites.options_12x16,
                                                         new string[] { GameResources.options_12x16_0, GameResources.options_12x16_1 },
                                                         GameResources.options_12x16_2, 350, Game.font4));

            fontSizeMenu.SelectedMenu = GameSettings.Instance.FontSize;

            // FONT SIZE MENU
            b = new Text(23, 16, "Demo mode:", Color.Black);
            TGE.Instance.SView.Add("text_demo", b);

            Menu demoModeMenu = new Menu(72, 15);
            a = new Sprite(new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.options_indicator)));
            TGE.Instance.SView.Add(GameSprites.options_indicator + "_2", a);

            demoModeMenu.SelectedIndicator = a;


            demoModeMenu.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.options_off,
                                                         new string[] { GameResources.options_off0, GameResources.options_off1 },
                                                         GameResources.options_off2, 350, demoOff));

            demoModeMenu.AddItem(ButtonFactory.NewButton(20, 0, GameSprites.options_on,
                                             new string[] { GameResources.options_on0, GameResources.options_on1 },
                                             GameResources.options_on2, 350, demoOn));


            demoModeMenu.SelectedMenu = GameSettings.Instance.DemoMode;

            // BACK BUTTON
            goBack = ButtonFactory.NewButton(90, 23, GameSprites.options_back,
                                             new string[] { GameResources.options_back0, GameResources.options_back1 },
                                             GameResources.options_back2, 350, Exit);

            // BACK BUTTON
            resetGame = ButtonFactory.NewButton(87, 34, GameSprites.options_reset,
                                                 new string[] { GameResources.options_reset0, GameResources.options_reset1 },
                                                 GameResources.options_reset2, 350, resetGameFunc);

            optionsMenu = new MultiLevelMenu();
            optionsMenu.Add(fontSizeMenu);
            optionsMenu.Add(demoModeMenu);
            optionsMenu.Add(goBack);
            optionsMenu.Add(resetGame);


            b = new Text(80, 2, "Game options", Color.FromArgb(0, 136, 0, 21));
            TGE.Instance.SView.Add("text_header", b);

            b = new Text(5, 39, "Info:", Color.Black);
            TGE.Instance.SView.Add("text_info", b);

            Color infoColor = Color.BlueViolet;

            b = new Text(5, 43, "* Character size determines console's font size.", infoColor);
            TGE.Instance.SView.Add("text_info_content_line0", b);

            b = new Text(5, 46, "Each character has two 'virtual' pixels. Setting", infoColor);
            TGE.Instance.SView.Add("text_info_content_line1", b);

            b = new Text(5, 49, "e.g.: 4x6 means that each virtual pixel is 4 screen", infoColor);
            TGE.Instance.SView.Add("text_info_content_line2", b);

            b = new Text(5, 52, "pixels wide and 3 (6/2) screen pixels height.", infoColor);
            TGE.Instance.SView.Add("text_info_content_line3", b);



            b = new Text(5, 57, "* Demo mode option enables You to activate special", infoColor);
            TGE.Instance.SView.Add("text_info_content_line5", b);

            b = new Text(5, 60, "test mode, which will speed up game.", infoColor);
            TGE.Instance.SView.Add("text_info_content_line6", b);

        }

        protected override void inLoop()
        {

        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            optionsMenu.KeyPressed(cKey);

            switch (cKey)
            {
                case ConsoleKey.Escape:
                    Exit();

                    break;

                default:
                    break;
            }
        }

        protected override void onExit()
        {
            serializer.SerializeSettings(GameSettings.Instance);
        }
    }
}
