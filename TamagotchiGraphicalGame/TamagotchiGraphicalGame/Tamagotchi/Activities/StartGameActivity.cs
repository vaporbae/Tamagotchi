using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class StartGameActivity : Activity
    {
        

        private Menu optionMenu;

        Text[] nameText;
        int currentCharacter;
        Sprite coursor;

        private string getName()
        {
            if (nameText == null)
                return Game.Tamagotchi.Name;

            string name = "";

            foreach (Text txt in nameText)
            {
                name += txt.Content;
            }

            return name;
        }

        private void goToNewGame()
        {
            string name = getName();
            if (name != "")
            {
                Game.Tamagotchi = new Character(name);

                save();

                GoToActivity(new GameMainActivity());

                Exit();
            }
        }

        private void save()
        {
            Serializer serializer = new Serializer();
            serializer.Serialize(Game.Tamagotchi);
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

            Serializer serializer = new Serializer();
            if (serializer.Exists() == false)
            {
                Text text = new Text(58, 16, "Character name:", Color.Black);
                TGE.Instance.SView.Add("text_label", text);


                for (int i = 0; i < 8; ++i)
                    a = SpriteFactory.NewSprite((short)(58 + 14 * i), 22, GameSprites.name_char_field + i.ToString(), new string[] { GameResources.name_char_field }, null);

                nameText = new Text[8];
                currentCharacter = 0;

                for (int i = 0; i < 8; ++i)
                {
                    nameText[i] = new Text((short)(62 + 14 * i), 24, "", Color.Black);
                    TGE.Instance.SView.Add("name_text_characters_" + i.ToString(), nameText[i]);
                }

                coursor = SpriteFactory.NewSprite(59, 27, GameSprites.name_char_coursor, new string[] { GameResources.name_char_coursor0, GameResources.name_char_coursor1 }, null);
                coursor.AnimationEnabled = true;


                optionMenu = new Menu(85, 35);

                optionMenu.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.game_start_confirm,
                                                             new string[] { GameResources.game_start_confirm0, GameResources.game_start_confirm1 },
                                                             GameResources.game_start_confirm1, 350, goToNewGame), true);

                optionMenu.AddItem(ButtonFactory.NewButton(30, 0, GameSprites.game_start_exit,
                                                             new string[] { GameResources.game_start_exit0, GameResources.game_start_exit1 },
                                                             GameResources.game_start_exit1, 350, Exit));
            }
            else
            {
                Game.Tamagotchi = serializer.Deserialize();


                GoToActivity(new GameMainActivity());
                Exit();
            }

        }

        protected override void inLoop()
        {

        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            optionMenu.KeyPressed(cKey);

            if (cKey >= ConsoleKey.A && cKey <= ConsoleKey.Z)
            {
                if (currentCharacter < 8)
                {
                    char key = (char)(cKey - ConsoleKey.A + 'A');

                    nameText[currentCharacter].Content = key.ToString();
                    if (++currentCharacter == 8)
                        currentCharacter = 8;

                    if (currentCharacter != 8)
                        coursor.Position.X += 14;
                }
            }

            if (currentCharacter <= 8 && cKey == ConsoleKey.Backspace)
            {
                if (currentCharacter != 0)
                {
                    if (currentCharacter != 8)
                        coursor.Position.X -= 14;

                    nameText[currentCharacter - 1].Content = "";
                    if (--currentCharacter < 0)
                        currentCharacter = 0;
                }
            }
        }

        protected override void onExit()
        {

        }
    }
}
