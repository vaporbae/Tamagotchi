using System;
using TextGraphicsEngine;

namespace TamagotchiConsoleGame.UI
{
    public abstract class Activity
    {
        private bool stayInActivity = true;

        public void Execute(Activity sender)
        {
            stayInActivity = true;

            onCreate(sender);

            while (stayInActivity)
            {
                TGE.Instance.Render();

                inLoop();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ConsoleKey cKey = key.Key;

                    if (cKey == ConsoleKey.F10)
                        TGE.Instance.TakeScreenshot(false);
                    else if (cKey == ConsoleKey.F12)
                        TGE.Instance.TakeScreenshot(true);

                    onKeyPress(cKey);
                }
            }

            onExit();
        }

        protected void GoToActivity(Activity activity)
        {
            View prevView = TGE.Instance.SView;
            ScreenBuffer prevBuffer = TGE.Instance.SBuffer;

            TGE.Instance.SView = new View();
            TGE.Instance.SBuffer = new ScreenBuffer(prevBuffer.Width, prevBuffer.Height);

            activity.Execute(this);

            TGE.Instance.SView = prevView;
            TGE.Instance.SBuffer = prevBuffer;

            // due to bug with color managemnt it's neccesary to call full refresh
            TGE.Instance.Render(true);
        }

        protected void Exit()
        {
            stayInActivity = false;
        }

        protected virtual void onCreate(Activity sender)
        {

        }

        protected virtual void inLoop()
        {

        }

        protected virtual void onKeyPress(ConsoleKey cKey)
        {

        }

        protected virtual void onExit()
        {

        }
    }

}