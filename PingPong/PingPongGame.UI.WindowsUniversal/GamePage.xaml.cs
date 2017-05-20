using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using PingPong.Logic.Collision;
using PingPong.Logic.Engines;
using PingPong.Logic.Factories;
using PingPong.Logic.Renderers;
using PingPongGame.UI.WindowsUniversal.Providers;
using PingPongGame.UI.WindowsUniversal.Common;

namespace PingPongGame.UI.WindowsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private const double TimerTickIntervalInMilliseconds = 1;
        private const double PlayerStep = 15;
        private const double BallVerticalStep = 7.0;
        private const double BallHorizontalStep = 4.0;

        private IGame gameEngine;
        private IRenderer renderer;

        public GamePage()
        {
            this.InitializeComponent();

            this.CreateGame();
        }

        private void CreateGame()
        {
            var imageProvider = new ImageProvider();
            this.renderer = new WURenderer(this.TheCanvas, imageProvider, Constants.FieldWidth, Constants.FieldHeight);
            var factory = new GameObjectsFactory();
            var collisionDetector = new SimpleCollisionDetector();
            this.gameEngine = new TwoPlayersGame(renderer, factory, collisionDetector);

            gameEngine.InitGame(BallVerticalStep, BallHorizontalStep, PlayerStep);

            var timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(TimerTickIntervalInMilliseconds)
            };
            timer.Tick += StartGame;
            timer.Start();
        }

        private void StartGame(object sender, object e)
        {
            this.gameEngine.StartGame();

            this.FirstPlayerScoreTB.Text = this.renderer.FirstPlayerScore.ToString();
            this.SecondPlayerScoreTB.Text = this.renderer.SecondPlayerScore.ToString();
            this.TheCanvas.Children.Add(this.FirstPlayerScoreTB);
            this.TheCanvas.Children.Add(this.SecondPlayerScoreTB);
        }
    }
}
