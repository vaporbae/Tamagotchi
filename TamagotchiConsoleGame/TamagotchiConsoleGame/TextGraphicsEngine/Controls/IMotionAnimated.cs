namespace TextGraphicsEngine.Controls
{
    interface IMotionAnimated
    {
        int? MotionAnimationRepeatCount { get; set; }

        int MotionStepDuration
        {
            get;
            set;
        }

        int MotionStepNumber
        {
            get;
            set;
        }

        int MotionAnimationTimer
        {
            get;
            set;
        }

        bool MotionAnimationEnabled
        {
            get;
            set;
        }
    }
}
