
namespace EndGate.Server.MovementControllers
{
    public class MoveEvent : IMoveEvent
    {
        public string Direction { get; set; }
        public bool StartMoving { get; set; }

        public MoveEvent(string direction, bool startMoving)
        {
            Direction = direction;
            StartMoving = startMoving;
        }
    }
}
