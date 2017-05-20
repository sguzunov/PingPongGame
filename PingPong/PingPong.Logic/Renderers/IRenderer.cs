using System;

using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Enums;

namespace PingPong.Logic.Renderers
{
    public interface IRenderer
    {
        event EventHandler<UiActionEventArgs> PlayerActionHappend;

        int FirstPlayerScore { get; }

        int SecondPlayerScore { get; }

        double FieldWidth { get; }

        double FieldHeight { get; }

        void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer);

        void DrawBall(IBall ball);

        void Clear();

        void UpdateScore(PlayerInAction player, int score);
    }
}
