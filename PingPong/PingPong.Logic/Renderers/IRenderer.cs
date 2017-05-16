using System;

using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Renderers
{
    public interface IRenderer
    {
        event EventHandler<UiActionEventArgs> PlayerActionHappend;

        double FieldWidth { get; }

        double FieldHeight { get; }

        void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer);

        void DrawBall(IBall ball);

        void Clear();
    }
}
