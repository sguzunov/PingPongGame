using System.Windows;

using PingPong.Logic.Engines;
using PingPong.Logic.Factories;
using PingPong.UI.Wpf.Helpers;
using System.Windows.Threading;
using PingPong.Logic.Collision;
using System;
using PingPong.Logic.Renderers;

namespace PingPong.UI.Wpf
{
    public partial class GameWindow : Window
    {
        private const double TimerTickIntervalInMilliseconds = 1;
        private const double PlayerStep = 10;
        private const double BallVerticalStep = 2;
        private const double BallHorizontalStep = 2;

        private IGame gameEngine;
        private IRenderer renderer;

        public GameWindow()
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

            gameEngine.InitGame(BallVerticalStep, BallHorizontalStep, PlayerStep);

            var timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(TimerTickIntervalInMilliseconds)
            };
            timer.Tick += LoopGame;
            timer.Start();
        }

        private void LoopGame(object sender, System.EventArgs e)
        {
            this.gameEngine.LoopGame();

            this.FirstPlayerScoreTB.Text = this.renderer.FirstPlayerScore.ToString();
            this.SecondPlayerScoreTB.Text = this.renderer.SecondPlayerScore.ToString();
            this.TheCanvas.Children.Add(this.FirstPlayerScoreTB);
            this.TheCanvas.Children.Add(this.SecondPlayerScoreTB);
        }
    }
}
