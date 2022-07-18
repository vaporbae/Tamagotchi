using static TextGraphicsEngine.Controls.TGEEvents;

namespace TextGraphicsEngine.Controls
{
    interface IAnimated
    {
        int AnimationFrameDuration
        {
            get;
            set;
        }

        int AnimationFrame
        {
            get;
            set;
        }

        int? AnimationRepeatCount
        {
            get;
            set;
        }


        int AnimationTimer
        {
            get;
            set;
        }

        bool AnimationEnabled
        {
            get;
            set;
        }
    }
}
