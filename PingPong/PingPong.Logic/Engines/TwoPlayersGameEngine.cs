using System;
using System.Collections.Generic;

using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Factories;
using PingPong.Logic.GameObjects;
using PingPong.Logic.Renderers;

namespace PingPong.Logic.Engines
{
    public class TwoPlayersGameEngine : IGameEngine
    {
        private const int BallRadius = 5;
        private const int BallSpeed = 5;

        private readonly IEnumerable<IGameObject> objects;
        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private IBall ball;
        private readonly IGameObjectsFactory objectsFactory;
        private readonly IRenderer renderer;

        public TwoPlayersGameEngine(IRenderer renderer, IGameObjectsFactory objectsFactory)
        {
            this.renderer = renderer;
            this.objectsFactory = objectsFactory;

            this.objects = new List<IGameObject>();
        }

        public event EventHandler GameFinished;

        public void InitGame()
        {
            var ballPosition = new Position(this.renderer.FieldHeight / 2, this.renderer.FieldWidth / 2);
            this.ball = this.objectsFactory.CreateBall(ballPosition, BallRadius);

            // TODO: Create players

        }

        public void StartGame()
        {
            while (!this.IsGameFinished())
            {
                this.renderer.DrawObjects(this.objects));
            }

            this.OnGameFinished();
        }

        private void OnGameFinished()
        {
            this.GameFinished?.Invoke(null, null);
        }

        private bool IsGameFinished()
        {
            // TODO: Handle logic
            throw new NotImplementedException();
        }
    }
}
