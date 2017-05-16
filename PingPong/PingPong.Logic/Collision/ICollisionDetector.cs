using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Collision
{
    public interface ICollisionDetector
    {
        bool AreColliding(IPlayer player, IBall ball);
    }
}
