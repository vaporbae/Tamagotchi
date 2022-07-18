using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class FoodMenuActivity : Activity
    {
        MultiLevelMenu optionsMenu;
        private Menu firstRow, secondRow;

        Text price;

        GameMainActivity senderActivity;

        protected override void onCreate(Activity sender)
        {
            senderActivity = (GameMainActivity)sender;

            var a = SpriteFactory.NewSprite(0, 0, GameSprites.food_background, new string[] { GameResources.food_background }, null);

            firstRow = new Menu(22, 17);
            firstRow.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.bread_on,
                                                       new string[] { GameResources.bread_on, GameResources.bread_off },
                                                        GameResources.bread_off, 200, EatBread), true);
            firstRow.AddItem(ButtonFactory.NewButton(60, 0, GameSprites.cake_on,
                                                       new string[] { GameResources.cake_on, GameResources.cake_off },
                                                        GameResources.cake_off, 200, EatCake));
            firstRow.AddItem(ButtonFactory.NewButton(120, 0, GameSprites.carrot_on,
                                                       new string[] { GameResources.carrot_on, GameResources.carrot_off },
                                                        GameResources.carrot_off, 200, EatCarrot));
            secondRow = new Menu(22, 47);
            secondRow.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.ice_cream_on,
                                                       new string[] { GameResources.ice_cream_on, GameResources.ice_cream_off },
                                                        GameResources.ice_cream_off, 200, EatIceCream));
            secondRow.AddItem(ButtonFactory.NewButton(60, 0, GameSprites.meat_on,
                                                       new string[] { GameResources.meat_on, GameResources.meat_off },
                                                        GameResources.meat_off, 200, EatMeat));
            secondRow.AddItem(ButtonFactory.NewButton(120, 0, GameSprites.back_selected_on,
                                                       new string[] { GameResources.back_selected_on, GameResources.back_selected_off },
                                                        GameResources.back_selected_off, 200, Exit));

            Text coins = new Text(178, 8, Game.Tamagotchi.Money.ToString(), Color.Black);
            coins.Align = TextAlignment.Right;
            TGE.Instance.SView.Add("text_coins", coins);

            price = new Text(99, 8, foodPrices[0, 0].ToString(), Color.Black);
            price.Align = TextAlignment.Right;
            TGE.Instance.SView.Add("text_price", price);


            optionsMenu = new MultiLevelMenu();
            optionsMenu.KeepSelected = true;
            optionsMenu.Add(firstRow);
            optionsMenu.Add(secondRow);
        }

        protected override void inLoop()
        {

        }

        private readonly int[,] foodPrices = new int[,] { { 25, 75, 150 }, { 300, 1000, 0 } };

        protected override void onKeyPress(ConsoleKey cKey)
        {
            optionsMenu.KeyPressed(cKey);

            int activeMenuIndex = optionsMenu.ActiveLevel;
            Menu activeMenu = (Menu)optionsMenu.Items[activeMenuIndex];
            int index = activeMenu.CurrrentMenu;

            price.Content = foodPrices[activeMenuIndex, index].ToString();

            if (price.Content == "0")
                price.Content = "---";
        }

        protected void EatBread()
        {
            Sprite food = senderActivity.FoodSprite;
            if (Game.Tamagotchi.MoneyDown(foodPrices[0, 0]))
            {
                food.SpriteSequence.Clear();

                SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.bread0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.bread0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.bread1));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.bread2));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.bread3));
                food.SpriteSequence.Add(bmp);

                senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();

                food.IsActive = true;
                Exit();
            }
                //GoToActivity(new EatingBreadActivity());
        }

        protected void EatCake()
        {
            Sprite food = senderActivity.FoodSprite;
            if (Game.Tamagotchi.MoneyDown(foodPrices[0, 1]))
            {
                food.SpriteSequence.Clear();

                SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.cake0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.cake0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.cake1));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.cake2));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.cake3));
                food.SpriteSequence.Add(bmp);

                senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();

                food.IsActive = true;
                Exit();
            }


                //GoToActivity(new EatingCakeActivity());
        }

        protected void EatCarrot()
        {
            Sprite food = senderActivity.FoodSprite;
            if (Game.Tamagotchi.MoneyDown(foodPrices[0, 2]))
            {
                food.SpriteSequence.Clear();

                SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.carrot0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.carrot0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.carrot1));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.carrot2));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.carrot3));
                food.SpriteSequence.Add(bmp);

                senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();

                food.IsActive = true;
                Exit();
            }

            //    GoToActivity(new EatingCarrotActivity());
        }

        protected void EatIceCream()
        {
            Sprite food = senderActivity.FoodSprite;
            if (Game.Tamagotchi.MoneyDown(foodPrices[1, 0]))
            {
                food.SpriteSequence.Clear();

                SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.iceCream0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.iceCream0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.iceCream1));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.iceCream2));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.iceCream3));
                food.SpriteSequence.Add(bmp);

                senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();

                food.IsActive = true;
                Exit();
            }


            //    GoToActivity(new EatingIceCreamActivity());
        }

        protected void EatMeat()
        {
            Sprite food = senderActivity.FoodSprite;
            if (Game.Tamagotchi.MoneyDown(foodPrices[1, 1]))
            {
                food.SpriteSequence.Clear();

                SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.meat0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.meat0));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.meat1));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.meat2));
                food.SpriteSequence.Add(bmp);

                bmp = new SpriteBitmap(TGE.Instance.Resources.Get(GameResources.meat3));
                food.SpriteSequence.Add(bmp);

                senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();

                food.IsActive = true;
                Exit();
            }




            //if (Game.Tamagotchi.MoneyDown(foodPrices[1, 1]))
            //    GoToActivity(new EatingMeatActivity());
        }

        protected override void onExit()
        {

        }
    }
}
