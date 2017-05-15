using System;

namespace PingPong.Logic.Engines
{
    public interface IGameEngine
    {
        event EventHandler GameFinished;

        void InitGame();

        void StartGame();
    }
}
