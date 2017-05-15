using System.Collections.Generic;

using PingPong.Logic.GameObjects.Contracts;

namespace PingPong.Logic.Renderers
{
    public interface IRenderer
    {
        double FieldWidth { get; }

        double FieldHeight { get; }

        void DrawPlayers(IEnumerable<IPlayer> players);

        void DrawBall(IBall ball);
    }
}
