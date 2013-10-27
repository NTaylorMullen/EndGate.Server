
namespace EndGate.Server.MovementControllers
{
    public interface IMoveEvent
    {
        string Direction { get; set; }
        bool StartMoving { get; set; }
    }
}
