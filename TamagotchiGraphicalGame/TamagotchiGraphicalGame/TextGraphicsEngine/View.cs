using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TextGraphicsEngine.Controls;

namespace TextGraphicsEngine
{
    public sealed class View
    {
        private ConcurrentDictionary<string, OnScreenControl> onScreenObjects = new ConcurrentDictionary<string, OnScreenControl>();
        private List<OnScreenControl> onScreenObjectsList = new List<OnScreenControl>();

        private static System.Timers.Timer viewTimer;


        public View()
        {
            viewTimer = new System.Timers.Timer();
            viewTimer.Elapsed += new System.Timers.ElapsedEventHandler(viewTimerTick);
            viewTimer.Interval = 10;
            viewTimer.Enabled = true;
        }

        public void Render(ScreenBuffer buffer)
        {
            //ConsoleColorManager.RequiredColors.Clear();

            lock (onScreenObjectsList)
            {
                foreach (OnScreenControl obj in onScreenObjectsList)
                    obj.Draw(buffer);
            }
        }

        public OnScreenControl Add(string key, OnScreenControl obj)
        {
            lock (onScreenObjectsList)
            {
                if (onScreenObjects.ContainsKey(key))
                    throw new ArgumentNullException("key", "Key already exists!");

                onScreenObjects.TryAdd(key, obj);


                onScreenObjectsList.Add(obj);
            }

            return obj;
        }

        public OnScreenControl Get(string key)
        {
            return onScreenObjects[key];
        }

        public void Remove(string key)
        {
            OnScreenControl removedObj;
            onScreenObjects.TryRemove(key, out removedObj);

            lock (onScreenObjectsList)
            {
                onScreenObjectsList.Remove(removedObj);
            }
        }

        public void PauseView(bool pause)
        {
            viewTimer.Enabled = !pause;
        }

        private void viewTimerTick(object source, System.Timers.ElapsedEventArgs e)
        {
            lock (onScreenObjectsList)
            {
                int max = onScreenObjects.Count;

                for(int i = 0; i < max; ++i)
                {

                    OnScreenControl obj = onScreenObjectsList[i];

                    if (obj is IAnimated)
                    {
                        IAnimated animObj = (IAnimated)obj;

                        --animObj.AnimationTimer;
                    }

                    if (obj is IMotionAnimated)
                    {
                        IMotionAnimated motionAnimObj = (IMotionAnimated)obj;

                        --motionAnimObj.MotionAnimationTimer;
                    }
                }
            }
        }
    }
}
