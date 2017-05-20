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

        public IBall CreateBall(double top, double left, int radius)
        {
            var position = new Position(top, left);
            return this.CreateBall(position, radius);
        }

        public IPlayer CreatePlayer(Position startPosition, Bounds size)
        {
            var player = new Player(startPosition, size);
            return player;
        }

        public IPlayer CreatePlayer(double top, double left, double width, double height)
        {
            var position = new Position(top, left);
            var size = new Bounds(width, height);
            return this.CreatePlayer(position, size);
        }
    }
}
