using System.Windows;

using PingPong.Logic.Engines;
using PingPong.Logic.Factories;
using PingPong.UI.Wpf.Helpers;
using System.Windows.Threading;
using PingPong.Logic.Collision;
using System;
using PingPong.Logic.Renderers;

namespace PingPong.UI.Wpf.Views
{
    public partial class GameView : Window
    {
        private IGame gameEngine;
        private IRenderer renderer;
        private const double TimerTickIntervalInMilliseconds = 1;

        public GameView()
        {
            this.InitializeComponent();

            this.CreateGame();
        }

        private void CreateGame()
        {
            var imageProvider = new ImageProvider();
            this.renderer = new WpfRenderer(this.TheCanvas, imageProvider);
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
            this.FirstPlayerScoreTextBox.Text = this.renderer.FirstPlayerScore.ToString();
            this.SecondPlayerScoreTextBox.Text = this.renderer.SecondPlayerScore.ToString();
        }
    }
}
