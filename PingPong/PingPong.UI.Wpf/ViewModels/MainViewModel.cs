using System.Windows.Input;

using PingPong.UI.Wpf.Behaviour;
using PingPong.UI.Wpf.ViewModels.Base;

namespace PingPong.UI.Wpf.ViewModels
{
    public class MainViewModel : PageViewModel
    {
        private ICommand loadGame;

        public ICommand LoadGame => this.loadGame ?? (this.loadGame = new RelayCommand(this.HandleLoadNewGame));

        private void HandleLoadNewGame(object parameter)
        {
            this.OnViewChanged(new GameViewModel());
        }
    }
}
