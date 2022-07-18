using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity
    {
        private void loadExtraMenuButtons()
        {
            menu = new Menu(45, 67);

            TGE.Instance.SView.Remove(GameSprites.exit_button_on);

            menu.AddItem(ButtonFactory.NewButton(0, 0, GameSprites.stats_button_on,
                                                       new string[] { GameResources.stats_button_on },
                                                        GameResources.stats_button_off, 1000, enterStats), true);

            menu.AddItem(ButtonFactory.NewButton(19, 0, GameSprites.food_button_on,
                                                       new string[] { GameResources.food_button_on },
                                                        GameResources.food_button_off, 1000, enterFood));
            menu.AddItem(ButtonFactory.NewButton(38, 0, GameSprites.toilet_button_on,
                                                       new string[] { GameResources.toilet_button_on },
                                                        GameResources.toilet_button_off, 1000, toilet));
            menu.AddItem(ButtonFactory.NewButton(57, 0, GameSprites.game_button_on,
                                                       new string[] { GameResources.game_button_on },
                                                        GameResources.game_button_off, 1000, enterGame));
            menu.AddItem(ButtonFactory.NewButton(76, 0, GameSprites.emergency_button_on,
                                                       new string[] { GameResources.emergency_button_on },
                                                        GameResources.emergency_button_off, 1000, enterEmergency));
            menu.AddItem(ButtonFactory.NewButton(95, 0, GameSprites.action_button_on,
                                                       new string[] { GameResources.action_button_on },
                                                        GameResources.action_button_off, 1000, enterAction));

            menu.AddItem(ButtonFactory.NewButton(114, 0, GameSprites.exit_button_on,
                                                       new string[] { GameResources.exit_button_on },
                                                        GameResources.exit_button_off, 1000, Exit));
        }

        private int nextState = 1;
        private int nextCharacter = 1;


        //private void action()
        //{
        //    changeCharacter(Stage.Baby, "Shirobabychi");
        //    //throw new NotImplementedException();
        //}

        private void enterGame()
        {
            GoToActivity(new GameRopeActivity());
            save();
        }

        private void toilet()
        {
            if (poopSignal < 7)
            {
                //poop on toilet anim
                activateState("poop");
            }
            else
            {
                cleaning.IsActive = true;
                cleaning.MotionStepNumber = 0;
                cleaning.MotionAnimationEnabled = true;
            }

        }

        private void enterFood()
        {
            GoToActivity(new FoodMenuActivity());
        }

        private void enterStats()
        {
            GoToActivity(new GameStatsActivity());
        }

        private void enterEmergency()
        {
            activateState(ArrayOfStates[nextState]);
            nextState = (nextState + 1) % ArrayOfStates.Length;
        }

        private void enterAction()
        {
            changeCharacter(CharactersList[nextCharacter]);

            nextCharacter = (nextCharacter + 1) % CharactersList.Length;
        }
    }
}
