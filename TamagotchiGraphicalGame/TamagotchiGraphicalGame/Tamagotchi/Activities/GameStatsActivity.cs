using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class GameStatsActivity : Activity
    {

        Sprite a, b, c;
        Sprite nighta, nightb, nightc;
        protected override void onCreate(Activity sender)
        {

            a = SpriteFactory.NewSprite(0, 15, GameSprites.room_day_bg, new string[] { GameResources.room_day_bg }, null);
            a.IsActive = true;

            nighta = SpriteFactory.NewSprite(0, 15, GameSprites.room_night_bg, new string[] { GameResources.room_night_bg }, null);
            nighta.IsActive = false;

            c = SpriteFactory.NewSprite(0, 0, GameSprites.name_and_points_day, new string[] { GameResources.name_and_points_day }, null);
            c.IsActive = true;

            b = SpriteFactory.NewSprite(0, 65, GameSprites.actions_color_day, new string[] { GameResources.actions_color_day }, null);
            b.IsActive = true;


            nightb = SpriteFactory.NewSprite(0, 0, GameSprites.name_and_points_night, new string[] { GameResources.name_and_points_night }, null);
            nightb.IsActive = false;

            nightc = SpriteFactory.NewSprite(0, 65, GameSprites.action_color_night, new string[] { GameResources.actions_color_night }, null);
            nightc.IsActive = false;


            // get time from MainGameAActivity
            //dayOrNight();


            Text d = new Text(73, 8, Game.Tamagotchi.Name, Color.Black);
            d.Align = TextAlignment.Center;
            TGE.Instance.SView.Add("text_name", d);

            Text e = new Text(170, 8, Game.Tamagotchi.Money.ToString(), Color.Black);
            e.Align = TextAlignment.Right;
            TGE.Instance.SView.Add("text_money", e);

            Sprite f = SpriteFactory.NewSprite(30, 15, GameSprites.stats_table, new string[] { GameResources.stats_table }, null);

            Text g = new Text(107, 28, Game.Tamagotchi.Stage, Color.Black);
            TGE.Instance.SView.Add("text_stage", g);


            for (int i = 0; i < Game.Tamagotchi.Health; i++)
            {
                Sprite h = SpriteFactory.NewSprite((short)(108 + ( 9 * i )), 45, GameSprites.heart + "_health_" + i.ToString(), new string[] { GameResources.heart }, null);
            }

            for (int i = 0; i < Game.Tamagotchi.Happiness; i++)
            {
                Sprite j = SpriteFactory.NewSprite((short)(108 +  (9 * i )), 57, GameSprites.heart + "_happy_" + i.ToString(), new string[] { GameResources.heart }, null);
            }

            dayOrNight();
            //var j = new TextReverse(170, 7, Game.tamagotchi.Money.ToString(), Color.Black);
            //TGE.Instance.SView.Add("text_ch_size", e);
        }

        private void dayOrNight()
        {
            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour <= 8)
            {
                SetNight();
            }
            else
            {
                SetDay();
            }
        }

        protected void SetNight()
        {
            a.IsActive = false;
            b.IsActive = false;
            c.IsActive = false;
            nighta.IsActive = true;
            nightb.IsActive = true;
            nightc.IsActive = true;
        }

        protected void SetDay()
        {
            nighta.IsActive = false;
            nightb.IsActive = false;
            nightc.IsActive = false;
            a.IsActive = true;
            b.IsActive = true;
            c.IsActive = true;
        }


        protected override void inLoop()
        {

        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            Exit();
        }

        protected override void onExit()
        {

        }
    }
}
