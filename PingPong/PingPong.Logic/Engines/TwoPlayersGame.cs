using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Factories;
using PingPong.Logic.GameObjects;
using PingPong.Logic.Renderers;
using PingPong.Logic.Collision;
using PingPong.Logic.Enums;

namespace PingPong.Logic.Engines
{
    public class TwoPlayersGame : IGame
    {
        private const int BallRadius = 15;
        private const double PlayerWidth = 5;
        private const double PlayerHeight = 60;

        private readonly IGameObjectsFactory objectsFactory;
        private readonly IRenderer renderer;
        private readonly ICollisionDetector collisionDetector;

        private double playerStep;
        private double ballVerticalStep;
        private double ballHorizontalStep;

        private int firstPlayerScore;
        private int secondPlayerScore;

        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private IBall ball;

        public TwoPlayersGame(IRenderer renderer, IGameObjectsFactory objectsFactory, ICollisionDetector collisionDetector)
        {
            this.renderer = renderer;
            this.objectsFactory = objectsFactory;
            this.collisionDetector = collisionDetector;

            this.renderer.PlayerActionHappend += HandlePlayerAction;
        }

        public void InitGame(double ballVerticalStep, double ballHorizontalStep, double playerStep)
        {
            this.ballVerticalStep = ballVerticalStep;
            this.ballHorizontalStep = ballHorizontalStep;
            this.playerStep = playerStep;

            this.firstPlayerScore = 0;
            this.secondPlayerScore = 0;

            this.SetObjectsToInitialPositions();
        }

        public void StartGame()
        {
            this.UpdateBallPosition(this.ball);

            this.renderer.Clear();
            this.renderer.DrawBall(this.ball);
            this.renderer.DrawPlayers(this.firstPlayer, this.secondPlayer);

            if (this.IsRoundFinished())
            {
                this.SetObjectsToInitialPositions();
            }
        }

        private void SetObjectsToInitialPositions()
        {
            double ballTop = (this.renderer.FieldHeight / 2) - BallRadius;
            double ballLeft = (this.renderer.FieldWidth / 2) - BallRadius;
            this.ball = this.objectsFactory.CreateBall(ballTop, ballLeft, BallRadius);

            double fPlTop = (this.renderer.FieldHeight / 2) - (PlayerHeight / 2);
            double fPlLeft = 20;
            this.firstPlayer = this.objectsFactory.CreatePlayer(fPlTop, fPlLeft, PlayerWidth, PlayerHeight);

            double sPlTop = (this.renderer.FieldHeight / 2) - (PlayerHeight / 2);
            double sPlLeft = this.renderer.FieldWidth - 40;
            this.secondPlayer = this.objectsFactory.CreatePlayer(sPlTop, sPlLeft, PlayerWidth, PlayerHeight);
        }

        private void HandlePlayerAction(object sender, UiActionEventArgs e)
        {
            var action = e.PlayerAction;
            var playerInAction = action.Player == PlayerInAction.FirstPlayer ? this.firstPlayer : this.secondPlayer;
            this.UpdatePlayerPosition(playerInAction, action.Direction);
        }

        private void UpdatePlayerPosition(IPlayer player, Direction direction)
        {
            double update = this.playerStep * (int)direction;
            if ((player.Position.Top + update < 0)
                || (player.Position.Top + player.Size.Height + update + 25 >= this.renderer.FieldHeight))
            {
                return;
            }

            player.Position.Top += update;
        }

        private void UpdateBallPosition(IBall ball)
        {
            if ((ball.Position.Top + this.ballVerticalStep + ball.Radius * 3.5 >= this.renderer.FieldHeight) || (ball.Position.Top <= 0))
            {
                this.ballVerticalStep *= -1;
            }

            if (this.collisionDetector.AreColliding(this.firstPlayer, ball) ||
                this.collisionDetector.AreColliding(this.secondPlayer, ball))
            {
                this.ballHorizontalStep *= -1;
            }

            ball.Position.Top += this.ballVerticalStep;
            ball.Position.Left += this.ballHorizontalStep;
        }

        private bool IsRoundFinished()
        {
            if (this.ball.Position.Left + (this.ball.Radius / 2) >= this.renderer.FieldWidth)
            {
                this.firstPlayerScore++;
                return true;
            }

            if (this.ball.Position.Left + (this.ball.Radius / 2) < 0)
            {
                this.secondPlayerScore++;
                return true;
            }

            return false;
        }

        private IPlayer CreatePlayer(double topPosition, double leftPosition, Bounds size)
        {
            var playerPosition = new Position(topPosition, leftPosition);
            var player = this.objectsFactory.CreatePlayer(playerPosition, size);

            return player;
        }
    }
}
