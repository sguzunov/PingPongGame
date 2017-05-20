using System;

using PingPong.Logic.Enums;
using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Command;

namespace PingPong.Logic.Renderers
{
    public abstract class BaseGameRenderer : IRenderer
    {
        public int FirstPlayerScore { get; private set; }

        public int SecondPlayerScore { get; private set; }

        public abstract double FieldWidth { get; }

        public abstract double FieldHeight { get; }

        public event EventHandler<UiActionEventArgs> PlayerActionHappend;

        public abstract void Clear();

        public abstract void DrawBall(IBall ball);

        public abstract void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer);

        public abstract void ShowWinner(PlayerInAction winner);

        public void UpdateScore(PlayerInAction player, int score)
        {
            switch (player)
            {
                case PlayerInAction.FirstPlayer:
                    this.FirstPlayerScore = score;
                    break;
                case PlayerInAction.SecondPlayer:
                    this.SecondPlayerScore = score;
                    break;
                default:
                    break;
            }
        }

        protected void OnPlayerActionHappend(IPlayerAction action) => this.PlayerActionHappend?.Invoke(this, new UiActionEventArgs(action));
    }
}
