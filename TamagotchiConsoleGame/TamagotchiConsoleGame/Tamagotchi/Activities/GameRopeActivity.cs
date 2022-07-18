using System;
using System.Drawing;
using System.Threading;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    class GameRopeActivity : Activity
    {
        Sprite a, b, c;
        Sprite nighta, nightb, nightc;

        GameMainActivity senderActivity;

        private int[] jump = { 10, 20, 25, 30, 25, 20, 10, 0 };
        private short rope_frames = 16;
        int jump_iterator = 8;
        int rope_iterator = 0;
        Sprite character;
        Sprite rope;
        int fps_cap = 25;
        bool game_ended = false;
        int score = 0;
        Sprite end = null;
        Sprite countdown_sprite;
        bool countdown = true;

        protected override void onCreate(Activity sender)
        {
            senderActivity = (GameMainActivity)sender;

            initTimer();

            a = SpriteFactory.NewSprite(0, 15, GameSprites.room_day_bg, new string[] { GameResources.room_day_bg }, null);
            a.IsActive = false;

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



            dayOrNight();



            character = SpriteFactory.NewSprite(100, 50, GameResources.hashitamatchiBasicAnimNormal0, new string[] { GameResources.hashitamatchiBasicAnimNormal0 }, null);
            rope = SpriteFactory.NewSprite(40, 18, GameResources.rope0, new string[] {   GameResources.rope0,
                                                                                        GameResources.rope1,
                                                                                        GameResources.rope2,
                                                                                        GameResources.rope3,
                                                                                        GameResources.rope4,
                                                                                        GameResources.rope5,
                                                                                        GameResources.rope6,
                                                                                        GameResources.rope7,
                                                                                        GameResources.rope8,
                                                                                        GameResources.rope7,
                                                                                        GameResources.rope6,
                                                                                        GameResources.rope5,
                                                                                        GameResources.rope4,
                                                                                        GameResources.rope3,
                                                                                        GameResources.rope2,
                                                                                        GameResources.rope1 }, null, 200);
            var rope_handles = SpriteFactory.NewSprite(32, 42, GameResources.rope_handles, new string[] { GameResources.rope_handles }, null, 200);

            var d = new Text(70, 8, Game.Tamagotchi.Name, Color.Black);
            TGE.Instance.SView.Add("text_name", d);
            d.Align = TextAlignment.Center;

            var e = new Text(170, 8, Game.Tamagotchi.Money.ToString(), Color.Black);
            TGE.Instance.SView.Add("text_money", e);
            e.Align = TextAlignment.Right;

            countdown_sprite = SpriteFactory.NewSprite(90, 30, GameSprites.countdown_3, new string[] { GameResources.countdown_3, GameResources.countdown_2, GameResources.countdown_1 }, null);

            mainGameTimer.Enabled = true;
        }

        private void dayOrNight()
        {
            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour <= 8)
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

            //rainDay.IsActive = false;
            //rainNight.IsActive = true;
        }

        protected void SetDay()
        {
            nighta.IsActive = false;
            nightb.IsActive = false;
            nightc.IsActive = false;
            a.IsActive = true;
            b.IsActive = true;
            c.IsActive = true;

            //rainDay.IsActive = true;
            //rainNight.IsActive = false;
        }

        protected override void inLoop()
        {
            if (countdown)
            {
                if (countdown_sprite.AnimationFrame < 2)
                {
                    Thread.Sleep(10000 / fps_cap);
                    countdown_sprite.AnimationFrame++;
                }
                else
                {
                    countdown = false;
                    TGE.Instance.SView.Remove(GameSprites.countdown_3);
                }
            }
            else
            {
                if (game_ended)
                {
                    if (end == null)
                    {
                        end = SpriteFactory.NewSprite(30, 15, GameSprites.game_over, new string[] { GameResources.game_over }, null);

                        var score_text = new Text(140, 45, score.ToString(), Color.Black);
                        TGE.Instance.SView.Add("text_score_over", score_text);
                        score_text.Align = TextAlignment.Right;

                        var money_text = new Text(140, 57, (score * 10).ToString(), Color.Black);
                        TGE.Instance.SView.Add("text_money_over", money_text);
                        money_text.Align = TextAlignment.Right;

                        Game.Tamagotchi.MoneyUp(score * 10);
                    }
                }
                else
                {
                    if (rope_iterator == 8 && jump_iterator == jump.Length)
                    {
                        game_ended = true;
                    }
                    Thread.Sleep(1000 / fps_cap);
                    if (jump_iterator < jump.Length)
                    {
                        character.Position.Y = (short)(50 - jump[jump_iterator]);
                        jump_iterator++;
                    }

                    rope.AnimationFrame = rope_iterator;
                    if (rope_iterator >= rope_frames)
                    {
                        rope_iterator = 0;
                        score++;
                    }
                    rope_iterator++;
                }
            }
        }

        protected override void onKeyPress(ConsoleKey cKey)
        {
            switch (cKey)
            {
                case ConsoleKey.Spacebar:
                    if (jump_iterator >= jump.Length) jump_iterator = 0;
                    break;
                case ConsoleKey.Enter:
                    if (game_ended) Exit();
                    break;
                case ConsoleKey.Escape:
                    if (game_ended) Exit();
                    break;
                default:
                    break;
            }

            if (cKey == ConsoleKey.Spacebar)
            {

            }
        }

        protected override void onExit()
        {
            mainGameTimer.Enabled = false;

            senderActivity.MoneyText.Content = Game.Tamagotchi.Money.ToString();
        }

        private System.Timers.Timer mainGameTimer;

        private void initTimer()
        {
            mainGameTimer = new System.Timers.Timer();
            mainGameTimer.Elapsed += new System.Timers.ElapsedEventHandler(mainTimerTick);
            mainGameTimer.Interval = 5000;
            mainGameTimer.Enabled = false;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private void mainTimerTick(object source, System.Timers.ElapsedEventArgs e)
        {
            dayOrNight();
        }
    }
}