using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity : Activity
    {
        private Menu menu;

        private int poopSignal = 0;

        Sprite cleaning;
        Sprite a, b, c;
        Sprite nighta, nightb, nightc;

        Character character;
        Sprite characterHatching;

        Button exitButton;

        public Text MoneyText;
        public Sprite FoodSprite;

        Sprite rainDay, rainNight;

        private void cleaningEnded()
        {
            Game.Tamagotchi.Clean();
            cleaning.IsActive = false;
        }

        private void HideFood()
        {
            FoodSprite.AnimationEnabled = true;
            FoodSprite.AnimationRepeatCount = 1;
            FoodSprite.IsActive = false;
            activateState("glad");

            save();
        }

        protected override void onCreate(Activity sender)
        {
            initTimer();

            character = Game.Tamagotchi;

            rainDay = SpriteFactory.NewSprite(116, 23 - 100, GameSprites.window_rain_day, new string[] { GameResources.window_rain_day }, null);
            rainNight = SpriteFactory.NewSprite(116, 23 - 100, GameSprites.window_rain_night, new string[] { GameResources.window_rain_night }, null);
            rainDay.IsActive = false;
            rainNight.IsActive = false;


            for (int i = 0; i < 100; ++i)
                rainDay.MotionSteps.Add(new Coord(0, 1));
            rainDay.MotionSteps.Add(new Coord(0, -100));

            rainDay.MotionStepDuration = 60;
            rainDay.MotionAnimationEnabled = true;

            for (int i = 0; i < 100; ++i)
                rainNight.MotionSteps.Add(new Coord(0, 1));
            rainNight.MotionSteps.Add(new Coord(0, -100));

            rainNight.MotionStepDuration = 60;
            rainNight.MotionAnimationEnabled = true;

            a = SpriteFactory.NewSprite(0, 15, GameSprites.room_day_bg, new string[] { GameResources.room_day_bg }, null);
            a.IsActive = false;

            nighta = SpriteFactory.NewSprite(0, 15, GameSprites.room_night_bg, new string[] { GameResources.room_night_bg }, null);
            nighta.IsActive = false;

            initCharacterStates();
            characterHatching = SpriteFactory.NewSpriteC(initialChracterPostion, "characterHatching", new string[] { GameResources.boyV1WaitingToHatch0 }, null);
            characterHatching.IsActive = false;
            characterHatching.AnimationEnabled = true;

            FoodSprite = SpriteFactory.NewSprite(125, 47, "food", new string[] { GameResources.bread0 }, null, 500);
            FoodSprite.IsActive = false;
            FoodSprite.AnimationRepeatCount = 1;
            FoodSprite.AnimationEnabled = true;
            FoodSprite.OnAnimationEnd = HideFood;

            cleaning = SpriteFactory.NewSprite(0, 15 - 79, GameSprites.cleaning, new string[] { GameResources.cleaning }, null);

            c = SpriteFactory.NewSprite(0, 0, GameSprites.name_and_points_day, new string[] { GameResources.name_and_points_day }, null);
            c.IsActive = false;

            b = SpriteFactory.NewSprite(0, 65, GameSprites.actions_color_day, new string[] { GameResources.actions_color_day }, null);
            b.IsActive = false;


            nightb = SpriteFactory.NewSprite(0, 0, GameSprites.name_and_points_night, new string[] { GameResources.name_and_points_night }, null);
            nightb.IsActive = false;

            nightc = SpriteFactory.NewSprite(0, 65, GameSprites.action_color_night, new string[] { GameResources.actions_color_night }, null);
            nightc.IsActive = false;



            dayOrNight();



            menu = new Menu(45, 67);
            exitButton = ButtonFactory.NewButton(114, 0, GameSprites.exit_button_on,
                                                       new string[] { GameResources.exit_button_on },
                                                        GameResources.exit_button_off, 1000, Exit, true);
            menu.AddItem(exitButton);

            Text d = new Text(73, 8, Game.Tamagotchi.Name, Color.Black);
            d.Align = TextAlignment.Center;
            TGE.Instance.SView.Add("text_name", d);

            MoneyText = new Text(170, 8, Game.Tamagotchi.Money.ToString(), Color.Black);
            MoneyText.Align = TextAlignment.Right;
            TGE.Instance.SView.Add("text_money", MoneyText);


            cleaning.IsActive = false;
            cleaning.MotionAnimationEnabled = true;
            cleaning.MotionAnimationRepeatCount = 1;
            cleaning.OnMotionAnimationEnd = cleaningEnded;
            cleaning.MotionStepDuration = 30;

            for (int i = 0; i < 60; ++i)
                cleaning.MotionSteps.Add(new Coord(0, 1));
            cleaning.MotionSteps.Add(new Coord(0, -60));


            if (character.Stage == "Egg")
            {
                hatchingTime = GameSettings.Instance.DemoMode == 1 ? 1 : 24;
                eggToDisplay();
            }
            else
            {
                changeCharacter(Stage.Baby, "Babytchi");
                characterStates["basic_anim_normal"].IsActive = true;
                loadExtraMenuButtons();
            }

            mainGameTimer.Enabled = true; // must be in the last line of OnCreate
        }



        protected override void inLoop()
        {
            if (hatchingTime == 0)
            {
                hatchingTime = -1;
                hatch();
            }
        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            menu.KeyPressed(cKey);
        }

        protected override void onExit()
        {
            mainGameTimer.Enabled = false;
            save();
        }

        private void save()
        {
            Serializer serializer = new Serializer();
            serializer.Serialize(Game.Tamagotchi);
        }

    }
}
