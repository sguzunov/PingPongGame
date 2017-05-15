using PingPong.Logic.GameObjects;
using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Factories
{
    public interface IGameObjectsFactory
    {
        IPlayer CreatePlayer(Position startPosition, double size);

        IBall CreateBall(Position startPosition, int radius);
    }
}
