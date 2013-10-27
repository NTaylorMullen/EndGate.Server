
namespace EndGate.Server.MovementControllers
{
    public class LinearDirections
    {
        public LinearDirections()
        {
            Left = false;
            Right = false;
            Up = false;
            Down = false;
        }

        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }

        public static bool Valid(string direction)
        {
            return direction == "Left" || direction == "Right" || direction == "Up" || direction == "Down";
        }
    }
}
