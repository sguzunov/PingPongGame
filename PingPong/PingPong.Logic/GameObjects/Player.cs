using System;

using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.GameObjects
{
    public class Player : GameObject, IPlayer
    {
        public Player(Position initialPosition, double size)
            : base(initialPosition)
        {
            if (size <= 0.0)
            {
                throw new ArgumentException(nameof(size));
            }

            this.Size = size;
        }

        public double Size { get; }
    }
}
