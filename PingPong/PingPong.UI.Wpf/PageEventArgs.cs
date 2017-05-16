using System;

using PingPong.UI.Wpf.ViewModels.Base;

namespace PingPong.UI.Wpf
{
    public class PageEventArgs : EventArgs
    {
        public PageEventArgs(BaseViewModel view)
        {
            this.View = view;
        }

        public BaseViewModel View { get; set; }
    }
}
