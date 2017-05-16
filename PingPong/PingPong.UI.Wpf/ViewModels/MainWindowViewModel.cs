using PingPong.UI.Wpf.ViewModels.Base;

namespace PingPong.UI.Wpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;

        public MainWindowViewModel()
        {
            var mainView = new MainViewModel();
            mainView.ViewChanged += HandleViewChanged;

            this.CurrentViewModel = mainView;
        }

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private void HandleViewChanged(object sender, PageEventArgs e)
        {
            var newView = e.View as PageViewModel;
            if (newView != null)
            {
                newView.ViewChanged += HandleViewChanged;
                this.CurrentViewModel = newView;
            }
        }
    }
}
