using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Renderers;
using PingPong.UI.Wpf.Helpers;
using System.Windows.Input;
using PingPong.Logic.Command;
using PingPong.Logic.Enums;

namespace PingPong.UI.Wpf
{
    public class WpfRenderer : IRenderer
    {
        private const string BallImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Images\ball.png";
        private const string PlayerImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Images\player.png";

        private readonly Canvas canvas;
        private readonly Window parentElement;
        private TextBlock firstPlayerScore;
        private TextBlock secondPlayerScore;
        private readonly IImageProvider imageProvider;

        private Image ballImage;
        private Image firstPlImage;
        private Image secondPlImage;

        public event EventHandler<UiActionEventArgs> PlayerActionHappend;

        public WpfRenderer(Canvas canvas, IImageProvider imageProvider)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));

            this.parentElement = this.GetTopParent();
            this.parentElement.KeyDown += HandleKeyDown;

            this.firstPlayerScore = new TextBlock();
            this.secondPlayerScore = new TextBlock();

            this.firstPlayerScore.Text = 10.ToString();
            this.SetElementToPosition(this.firstPlayerScore, 10, 50);
            this.canvas.Children.Add(this.firstPlayerScore);
        }

        public double FieldWidth => this.parentElement.Width;

        public double FieldHeight => this.parentElement.Height;

        public void Clear()
        {
            this.canvas.Children.Clear();
        }

        public void DrawBall(IBall ball)
        {
            this.DrawObject(this.ballImage, BallImagePath, ball.Position.Top, ball.Position.Left, ball.Radius, ball.Radius);
        }

        public void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            this.DrawObject(this.firstPlImage,
                PlayerImagePath,
                firstPlayer.Position.Top,
                firstPlayer.Position.Left,
                firstPlayer.Size.Width,
                firstPlayer.Size.Height);

            this.DrawObject(this.secondPlImage,
                PlayerImagePath,
                secondPlayer.Position.Top,
                secondPlayer.Position.Left,
                secondPlayer.Size.Width,
                secondPlayer.Size.Height);
        }

        private void DrawObject(UIElement element, string imagePath, double topPosition, double leftPosition, double width, double height)
        {
            if (element == null)
            {
                element = this.imageProvider.GetImageFromPath(imagePath, width, height);
            }

            this.SetElementToPosition(element, topPosition, leftPosition);
            this.canvas.Children.Add(element);
        }

        private void SetElementToPosition(UIElement element, double topPosition, double leftPosition)
        {
            Canvas.SetTop(element, topPosition);
            Canvas.SetLeft(element, leftPosition);
        }

        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            IPlayerAction action = null;
            if (e.Key == Key.W)
            {
                action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Up);
            }
            else if (e.Key == Key.S)
            {
                action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Down);
            }
            else if (e.Key == Key.Up)
            {
                action = PlayerAction.CreatePlayerAction(PlayerInAction.SecondPlayer, Direction.Up);
            }
            else if (e.Key == Key.Down)
            {
                action = PlayerAction.CreatePlayerAction(PlayerInAction.SecondPlayer, Direction.Down);
            }
            else
            {
                return;
            }

            this.PlayerActionHappend(this, new UiActionEventArgs(action));
        }

        private Window GetTopParent()
        {
            var parent = VisualTreeHelper.GetParent(this.canvas);
            while (!(parent is Window))
            {
                parent = LogicalTreeHelper.GetParent(parent);

            }

            return parent as Window;
        }

        public void UpdateScore(PlayerInAction player, int score)
        {
            switch (player)
            {
                case PlayerInAction.FirstPlayer:
                    this.firstPlayerScore.Text = score.ToString();
                    break;
                case PlayerInAction.SecondPlayer:
                    this.secondPlayerScore.Text = score.ToString();
                    break;
                default:
                    break;
            }

            this.canvas.Children.Add(this.firstPlayerScore);
            this.canvas.Children.Add(this.secondPlayerScore);
        }
    }
}
