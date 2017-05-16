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
        private const double PlayerHeight = 50;
        private const double PlayerSpeed = 30;

        private readonly IGameObjectsFactory objectsFactory;
        private readonly IRenderer renderer;
        private readonly ICollisionDetector collisionDetector;

        private double BallSpeedVertical = 0.09;
        private double BallSpeedHorizontal = 0.09;

        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private IBall ball;

        public TwoPlayersGame(IRenderer renderer, IGameObjectsFactory objectsFactory, ICollisionDetector collisionDetector)
        {
            this.renderer = renderer;
            this.objectsFactory = objectsFactory;
            this.collisionDetector = collisionDetector;
        }

        public void InitGame()
        {
            var playerSize = new Bounds(PlayerWidth, PlayerHeight);

            double ballTop = (this.renderer.FieldHeight / 2) - BallRadius;
            double ballLeft = (this.renderer.FieldWidth / 2) - BallRadius;
            var ballPosition = new Position(ballTop, ballLeft);
            this.ball = this.objectsFactory.CreateBall(ballPosition, BallRadius);

            double fPlTop = (this.renderer.FieldHeight / 2) - (playerSize.Height / 2);
            double fPlLeft = 20;
            this.firstPlayer = this.CreatePlayer(fPlTop, fPlLeft, playerSize);

            double sPlTop = (this.renderer.FieldHeight / 2) - (playerSize.Height / 2);
            double sPlLeft = this.renderer.FieldWidth - 40;
            this.secondPlayer = this.CreatePlayer(sPlTop, sPlLeft, playerSize);

            this.renderer.PlayerActionHappend += HandlePlayerAction;
        }

        private void HandlePlayerAction(object sender, UiActionEventArgs e)
        {
            var action = e.PlayerAction;
            if (action.Player == PlayerInAction.FirstPlayer)
            {
                this.firstPlayer.Position.Top += (PlayerSpeed * (int)action.Direction);
            }
            else
            {
                this.secondPlayer.Position.Top += (PlayerSpeed * (int)action.Direction);
            }
        }

        public void StartGame()
        {
            this.renderer.Clear();

            // Update
            this.UpdateBallPosition(this.ball);

            // Draw
            this.renderer.DrawBall(this.ball);
            this.renderer.DrawPlayers(this.firstPlayer, this.secondPlayer);
        }

        private void UpdateBallPosition(IBall ball)
        {
            if ((ball.Position.Top + BallSpeedVertical + ball.Radius * 3.5 >= this.renderer.FieldHeight) || (ball.Position.Top <= 0))
            {
                BallSpeedVertical *= -1;
            }

            if (this.collisionDetector.AreColliding(this.firstPlayer, ball) ||
                this.collisionDetector.AreColliding(this.secondPlayer, ball))
            {
                BallSpeedHorizontal *= -1;
            }

            ball.Position.Top += BallSpeedVertical;
            ball.Position.Left += BallSpeedHorizontal;
        }

        private bool IsGameFinished()
        {
            return (this.ball.Position.Left + (this.ball.Radius / 2) >= this.renderer.FieldWidth) ||
                (this.ball.Position.Left + (this.ball.Radius / 2) < 0);
        }

        private IPlayer CreatePlayer(double topPosition, double leftPosition, Bounds size)
        {
            var playerPosition = new Position(topPosition, leftPosition);
            var player = this.objectsFactory.CreatePlayer(playerPosition, size);

            return player;
        }
    }
}
