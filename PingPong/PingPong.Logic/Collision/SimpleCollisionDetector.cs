using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Collision
{
    public class SimpleCollisionDetector : ICollisionDetector
    {
        public bool AreColliding(IPlayer player, IBall ball)
        {
            double playerTop = player.Position.Top;
            double playerLeft = player.Position.Left;
            double playerBottom = player.Position.Top + player.Size.Height;
            double playerRight = player.Position.Left + player.Size.Width;

            double ballTop = ball.Position.Top;
            double ballLeft = ball.Position.Left;
            double ballBottom = ball.Position.Top + ball.Radius;
            double ballRight = ball.Position.Left + ball.Radius;

            bool fromPlayerBottom = ((playerTop <= ballTop && ballTop <= playerBottom) || (playerTop <= ballBottom && ballBottom <= playerBottom)) &&
                ((playerLeft <= ballLeft && ballLeft <= playerRight) || (playerLeft <= ballRight && ballRight <= playerRight));

            bool fromBallBottom = ((ballTop <= playerTop && playerTop <= ballBottom) || (ballTop <= playerBottom && playerBottom <= ballBottom)) &&
                ((ballLeft <= playerLeft && playerLeft <= ballRight) || (ballLeft <= playerRight && playerRight <= ballRight));

            return fromPlayerBottom || fromBallBottom;
        }
    }
}
