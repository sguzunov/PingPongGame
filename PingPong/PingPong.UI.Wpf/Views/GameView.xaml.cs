using System.Windows;

using PingPong.Logic.Engines;
using PingPong.Logic.Factories;
using PingPong.UI.Wpf.Helpers;
using System.Windows.Threading;
using PingPong.Logic.Collision;
using System;

namespace PingPong.UI.Wpf.Views
{
    public partial class GameView : Window
    {
        private IGame gameEngine;
        private const double TimerTickIntervalInMilliseconds = 1;

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
            var collisionDetector = new SimpleCollisionDetector();
            this.gameEngine = new TwoPlayersGame(renderer, factory, collisionDetector);

            gameEngine.InitGame();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(TimerTickIntervalInMilliseconds);
            timer.Tick += StartGame;
            timer.Start();
        }

        private void StartGame(object sender, System.EventArgs e)
        {
            this.gameEngine.StartGame();
        }
    }
}
