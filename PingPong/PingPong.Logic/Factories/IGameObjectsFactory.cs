using PingPong.Logic.GameObjects;
using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Factories
{
    public interface IGameObjectsFactory
    {
        IPlayer CreatePlayer(Position startPosition, Bounds size);

        IPlayer CreatePlayer(double top, double left, double width, double height);

        IBall CreateBall(Position startPosition, int radius);

        IBall CreateBall(double top, double left, int radius);
    }
}
