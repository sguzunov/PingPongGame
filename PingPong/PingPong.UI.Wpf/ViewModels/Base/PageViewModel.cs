using System;

namespace PingPong.UI.Wpf.ViewModels.Base
{
    public abstract class PageViewModel : BaseViewModel
    {
        public event EventHandler<PageEventArgs> ViewChanged;

        protected void OnViewChanged(BaseViewModel view) => this.ViewChanged?.Invoke(this, new PageEventArgs(view));
    }
}
