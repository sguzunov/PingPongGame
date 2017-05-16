using PingPong.Logic.GameObjects;
using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Factories
{
    public class GameObjectsFactory : IGameObjectsFactory
    {
        public IBall CreateBall(Position startPosition, int radius)
        {
            var ball = new Ball(startPosition, radius);
            return ball;
        }

        public IPlayer CreatePlayer(Position startPosition, Bounds size)
        {
            var player = new Player(startPosition, size);
            return player;
        }
    }
}
