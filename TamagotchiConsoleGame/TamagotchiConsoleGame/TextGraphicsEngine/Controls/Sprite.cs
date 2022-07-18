using System.Collections.Generic;
using TextGraphicsEngine.Misc;
using static TextGraphicsEngine.Controls.TGEEvents;

namespace TextGraphicsEngine.Controls
{
    public class Sprite : OnScreenControl, IAnimated, IMotionAnimated
    {
        public List<SpriteBitmap> SpriteSequence;

        // ANIMATION ==============================
        public int AnimationFrameDuration { get; set; } = 250;

        private int animationFrame = 0;

        public int AnimationFrame
        {
            get { return animationFrame; }
            set
            {
                animationFrame = value % SpriteSequence.Count;
            }
        }

        public OnSpriteAnimationEnd OnAnimationEnd;
        public OnSpriteAnimationLastFrame OnLastFrame;

        private int? animRepeatCount = null;
        private int? animationRepeatCountSaved = null;
        public int? AnimationRepeatCount
        {
            get { return animRepeatCount; }
            set
            {
                animRepeatCount = value;
                animationRepeatCountSaved = value;
            }
        }

        private int animationTimer;
        public int AnimationTimer
        {
            get { return animationTimer; }
            set
            {
                if (AnimationEnabled == true && IsActive && (animRepeatCount > 0 || animRepeatCount == null))
                {

                    if (value >= 0)
                        animationTimer = value;

                    if (animationTimer == 0)
                    {
                        // prepare next frame
                        do
                        {
                            ++AnimationFrame;

                        } while (SpriteSequence[AnimationFrame].IsAvaiable == false && AnimationFrame != 0);

                        if (AnimationFrame == 0)
                        {
                            if (animRepeatCount != null)
                                --animRepeatCount;

                            OnLastFrame?.Invoke();
                        }

                        animationTimer = AnimationFrameDuration / 10;
                    }

                    if (animRepeatCount == 0)
                    {
                        animationTimerEnabled = false;
                        animRepeatCount = animationRepeatCountSaved;

                        OnAnimationEnd?.Invoke();
                    }
                }
            }
        }

        private bool animationTimerEnabled = false;
        public bool AnimationEnabled
        {
            get { return animationTimerEnabled; }
            set
            {
                animationTimerEnabled = value;

                if (AnimationEnabled == false)
                    animationTimer = AnimationFrameDuration / 10;
            }

        }
        // ========================================

        // MOTION ANIMATION ==============================
        public List<Coord> MotionSteps;
        public int MotionStepDuration { get; set; } = 250;

        private int motionStepNumber = 0;

        public int MotionStepNumber
        {
            get { return motionStepNumber; }
            set
            {
                motionStepNumber = value % MotionSteps.Count;
            }
        }

        public OnSpriteAnimationEnd OnMotionAnimationEnd;
        public OnSpriteAnimationLastFrame OnMotionLastStep;

        private int? motionAnimRepeatCount = null;
        private int? motionAnimationRepeatCountSaved = null;


        public int? MotionAnimationRepeatCount
        {
            get
            {
                return motionAnimRepeatCount;
            }
            set
            {
                motionAnimRepeatCount = value;
                motionAnimationRepeatCountSaved = value;
            }
        }

        private int motionAnimationTimer;
        public int MotionAnimationTimer
        {
            get { return motionAnimationTimer; }
            set
            {
                if (MotionAnimationEnabled == true && IsActive == true && (motionAnimRepeatCount > 0 || motionAnimRepeatCount == null))
                {
                    if (value >= 0)
                        motionAnimationTimer = value;

                    if (motionAnimationTimer == 0)
                    {
                        Position.X += MotionSteps[MotionStepNumber].X;
                        Position.Y += MotionSteps[MotionStepNumber].Y;

                        ++MotionStepNumber;

                        if (MotionStepNumber == 0)
                        {
                            if (motionAnimRepeatCount != null)
                                --motionAnimRepeatCount;

                            OnMotionLastStep?.Invoke();
                        }

                        motionAnimationTimer = MotionStepDuration / 10;
                    }

                    if (motionAnimRepeatCount == 0)
                    {
                        motionAnimationTimerEnabled = false;
                        motionAnimRepeatCount = motionAnimationRepeatCountSaved;

                        OnMotionAnimationEnd?.Invoke();
                    }
                }
            }

        }

        private bool motionAnimationTimerEnabled = false;
        public bool MotionAnimationEnabled
        {
            get { return motionAnimationTimerEnabled; }
            set
            {
                motionAnimationTimerEnabled = value;

                if (MotionAnimationEnabled == false)
                    motionAnimationTimer = AnimationFrameDuration / 10;
            }

        }
        // ==============================================



        public Sprite()
        {
            Position = new Coord(0, 0);
            SpriteSequence = new List<SpriteBitmap>();
            MotionSteps = new List<Coord>();
        }

        public Sprite(int frameDuration) : this()
        {
            AnimationFrameDuration = frameDuration;
        }

        public Sprite(SpriteBitmap spriteBitmap) : this()
        {
            SpriteSequence.Add(spriteBitmap);
        }

        public Sprite(SpriteBitmap spriteBitmap, int frameDuration) : this(frameDuration)
        {
            SpriteSequence.Add(spriteBitmap);
        }

        public Sprite(short X, short Y)
        {
            Position = new Coord(X, Y);
            SpriteSequence = new List<SpriteBitmap>();
            MotionSteps = new List<Coord>();
        }

        public Sprite(short X, short Y, int frameDuration)
        {
            Position = new Coord(X, Y);
            SpriteSequence = new List<SpriteBitmap>();
            MotionSteps = new List<Coord>();

            AnimationFrameDuration = frameDuration;
        }

        public Sprite(short X, short Y, SpriteBitmap spriteBitmap) : this(X, Y)
        {
            SpriteSequence.Add(spriteBitmap);
        }

        public Sprite(short X, short Y, SpriteBitmap spriteBitmap, int frameDuration) : this(X, Y, frameDuration)
        {
            SpriteSequence.Add(spriteBitmap);
        }


        public Sprite(Coord position)
        {
            Position = new Coord(position);
            SpriteSequence = new List<SpriteBitmap>();
            MotionSteps = new List<Coord>();
        }

        public Sprite(Coord position, int frameDuration) : this(position)
        {
            AnimationFrameDuration = frameDuration;
        }

        public Sprite(Coord position, SpriteBitmap spriteBitmap) : this(position)
        {
            SpriteSequence.Add(spriteBitmap);
        }
        public Sprite(Coord position, SpriteBitmap spriteBitmap, int frameDuration) : this(position, frameDuration)
        {
            SpriteSequence.Add(spriteBitmap);
        }

        override public void Draw(ScreenBuffer buffer)
        {
            if (IsActive)
                SpriteSequence[animationFrame].Draw(buffer, Position);
        }

    }
}
