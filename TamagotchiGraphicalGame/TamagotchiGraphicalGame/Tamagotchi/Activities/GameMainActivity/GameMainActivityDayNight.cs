using System;
using System.Drawing;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity
    {
        private void dayOrNight()
        {
            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour <= 8)
            {
                SetNight();
                character.Tired = true;
            }
            else
            {
                SetDay();
                character.Tired = false;
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

            rainDay.IsActive = false;
            rainNight.IsActive = true;
        }

        protected void SetDay()
        {
            nighta.IsActive = false;
            nightb.IsActive = false;
            nightc.IsActive = false;
            a.IsActive = true;
            b.IsActive = true;
            c.IsActive = true;

            rainDay.IsActive = true;
            rainNight.IsActive = false;
        }
    }
}
