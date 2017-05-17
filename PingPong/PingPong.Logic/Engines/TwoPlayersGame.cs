using System;
using System.Collections.Generic;

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
        private const double PlayerStep = 10;

        private readonly IGameObjectsFactory objectsFactory;
        private readonly IRenderer renderer;
        private readonly ICollisionDetector collisionDetector;

        private double BallStepVertical = 2;
        private double BallStepdHorizontal = 2;

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

        public void InitGame()
        {
            this.firstPlayerScore = 0;
            this.secondPlayerScore = 0;

            this.SetObjectsToInitialPositions();
        }

        public void StartGame()
        {
            this.renderer.Clear();

            // Update
            this.UpdateBallPosition(this.ball);

            // Draw
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
            var ballPosition = new Position(ballTop, ballLeft);
            this.ball = this.objectsFactory.CreateBall(ballPosition, BallRadius);

            var playerSize = new Bounds(PlayerWidth, PlayerHeight);

            double fPlTop = (this.renderer.FieldHeight / 2) - (playerSize.Height / 2);
            double fPlLeft = 20;
            this.firstPlayer = this.CreatePlayer(fPlTop, fPlLeft, playerSize);

            double sPlTop = (this.renderer.FieldHeight / 2) - (playerSize.Height / 2);
            double sPlLeft = this.renderer.FieldWidth - 40;
            this.secondPlayer = this.CreatePlayer(sPlTop, sPlLeft, playerSize);
        }

        private void HandlePlayerAction(object sender, UiActionEventArgs e)
        {
            var action = e.PlayerAction;
            var playerInAction = action.Player == PlayerInAction.FirstPlayer ? this.firstPlayer : this.secondPlayer;
            this.UpdatePlayerPosition(playerInAction, action.Direction);
        }

        private void UpdatePlayerPosition(IPlayer player, Direction direction)
        {
            double update = PlayerStep * (int)direction;
            if ((player.Position.Top + update < 0)
                || (player.Position.Top + player.Size.Height + update + 25 >= this.renderer.FieldHeight))
            {
                return;
            }

            player.Position.Top += update;
        }

        private void UpdateBallPosition(IBall ball)
        {
            if ((ball.Position.Top + BallStepVertical + ball.Radius * 3.5 >= this.renderer.FieldHeight) || (ball.Position.Top <= 0))
            {
                BallStepVertical *= -1;
            }

            if (this.collisionDetector.AreColliding(this.firstPlayer, ball) ||
                this.collisionDetector.AreColliding(this.secondPlayer, ball))
            {
                BallStepdHorizontal *= -1;
            }

            ball.Position.Top += BallStepVertical;
            ball.Position.Left += BallStepdHorizontal;
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
