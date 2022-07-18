using System;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity
    {
        protected void death()
        {
            character.Alive = false;

            activateState("death");
        }

        protected void poopSignalize()
        {
            //signal to poop anim
            activateState("before_poop");
        }

        protected void poop()
        {
            //poop on the floor

            character.PoopCount++;
            //display poop
            //sorry anim
        }
        protected void checkCharacterState()
        {
            if (character.Health == 2 && !character.Ill)
            {
                //ill anim

                activateState("ill");

                character.Ill = true;
            }

            if (character.Health == 0 && !character.Ill)
            {
                //very ill anim
                activateState("very_ill");

                character.Ill = true;
            }

            if (character.Happiness == 2 && !character.Ill)
            {
                //random sad anim
                Random rnd = new Random();

                switch(rnd.Next(0, 2))
                {
                    case 0:
                        activateState("sad1");
                        break;
                    case 1:
                        activateState("sad2");
                        break;
                    case 2:
                        activateState("sad3");
                        break;
                }
            }

            if (character.Happiness == 0 && !character.Ill)
            {
                //cry anim
                activateState("cry");

            }

            if (character.Health == 4 && character.Happiness == 4)
            {
                //happy basic anim
                activateState("basic_anim_happy");
            }

        }
    }
}
