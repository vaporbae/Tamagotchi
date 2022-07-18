using System;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity
    {
        private System.Timers.Timer mainGameTimer;

        private DateTime night = new DateTime(2018, 10, 28, 21, 0, 0);
        private DateTime day = new DateTime(2018, 10, 28, 8, 0, 0);
        private int lifeDecrementTimer = 30; // this software timer will execute it's code in InLoop after 30*5s from activity start
        private int happinessDecrementTimer = 60;
        private int poopTimer = 70;
        private int hatchingTime = -1;


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
            checkCharacterState();


            if (hatchingTime > 0)
                --hatchingTime;

            if (lifeDecrementTimer != 0)
                --lifeDecrementTimer;

            if (happinessDecrementTimer != 0)
                --happinessDecrementTimer;

            if (poopTimer != 0)
            {
                --poopTimer;
            }

            if (poopSignal != 0)
            {
                --poopSignal;
            }

            // EVENT HANDLING


            // if time is over
            if (lifeDecrementTimer == 0)
            {
                lifeDecrementTimer = 30; // reset timer and set 30*5 = 150s
                if (character.Health == 0)
                {
                    death();
                }
                else
                    character.HealthDown();
            }

            if (happinessDecrementTimer == 0)
            {
                happinessDecrementTimer = 60;
                character.HappinessDown();
            }

            if (poopTimer == 0)
            {
                poopTimer = 80;
                poopSignalize();
                poopSignal = 6;

            }

            if (poopSignal == 0)
            {
                poopSignal = 1000;
                poop();
            }
        }
    }
}
