using TextGraphicsEngine.Misc;

namespace TextGraphicsEngine.Controls
{
    public abstract class OnScreenControl
    {
        public Coord Position;

        public bool IsActive = true;

        public abstract void Draw(ScreenBuffer buffer);
    }
}
