using System;

using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.GameObjects
{
    public class GameObject : IGameObject
    {
        private Position position;

        public GameObject(Position initialPosition)
        {
            this.Position = initialPosition;
        }

        public Position Position
        {
            get => this.position;

            set => this.position = value ?? throw new ArgumentNullException(nameof(position));
        }
    }
}
