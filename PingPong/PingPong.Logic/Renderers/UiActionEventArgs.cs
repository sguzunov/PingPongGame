using System;

using PingPong.Logic.Command;

namespace PingPong.Logic.Renderers
{
    public class UiActionEventArgs : EventArgs
    {
        public UiActionEventArgs(IPlayerAction playerAction)
        {
            this.PlayerAction = playerAction;
        }

        public IPlayerAction PlayerAction { get; }
    }
}
