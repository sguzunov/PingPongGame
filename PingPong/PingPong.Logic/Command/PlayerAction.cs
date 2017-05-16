using PingPong.Logic.Enums;

namespace PingPong.Logic.Command
{
    public class PlayerAction : IPlayerAction
    {
        private PlayerAction()
        {
        }

        public static IPlayerAction CreatePlayerAction(PlayerInAction player, Direction direction)
        {
            return new PlayerAction()
            {
                Player = player,
                Direction = direction
            };
        }

        public Direction Direction { get; private set; }

        public PlayerInAction Player { get; private set; }
    }
}
