using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.GameObjects
{
    public class Player : GameObject, IPlayer
    {
        public Player(Position initialPosition, Bounds size)
            : base(initialPosition)
        {
            this.Size = size;
        }

        public Bounds Size { get; }
    }
}
