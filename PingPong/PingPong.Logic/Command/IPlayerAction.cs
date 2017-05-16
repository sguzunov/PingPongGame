using PingPong.Logic.Enums;

namespace PingPong.Logic.Command
{
    public interface IPlayerAction
    {
        Direction Direction { get; }

        PlayerInAction Player { get; }
    }
}
