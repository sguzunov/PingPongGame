using System.Windows;

using PingPong.Logic.Engines;
using PingPong.Logic.Factories;
using PingPong.UI.Wpf.Helpers;
using System.Windows.Threading;

namespace PingPong.UI.Wpf.Views
{
    public partial class GameView : Window
    {
        private IGameEngine gameEngine;

        public GameView()
        {
            this.InitializeComponent();

            this.CreateGame();
        }

        private void CreateGame()
        {
            var imageProvider = new ImageProvider();
            var renderer = new WpfRenderer(this.TheCanvas, imageProvider);
            var factory = new GameObjectsFactory();
            this.gameEngine = new TwoPlayersGameEngine(renderer, factory);

            gameEngine.InitGame();

            var timer = new DispatcherTimer();
            timer.Tick += StartGame;
            timer.Start();

        }

        private void StartGame(object sender, System.EventArgs e)
        {
            this.gameEngine.StartGame();
        }
    }
}
