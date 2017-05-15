using System;

using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.GameObjects
{
    public class Ball : GameObject, IBall
    {
        public Ball(Position initialPosition, int radius) : base(initialPosition)
        {
            if (radius <= 0)
            {
                throw new ArgumentException(nameof(radius));
            }

            this.Radius = radius;
        }

        public double Radius { get; }
    }
}
