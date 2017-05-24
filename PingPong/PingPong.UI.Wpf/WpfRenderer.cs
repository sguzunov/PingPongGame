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
using System.Collections.Generic;

namespace PingPong.UI.Wpf
{
    public class WpfRenderer : BaseGameRenderer, IRenderer
    {
        private const string BallImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Assets\Game\ball.png";
        private const string PlayerImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Assets\Game\player.png";

        private readonly Canvas canvas;
        private readonly Window parentElement;
        private readonly IImageProvider imageProvider;
        private readonly ICollection<Key> pressedKeys;
        private readonly ICollection<Key> allowedKeys;

        private Image ballImage;
        private Image firstPlImage;
        private Image secondPlImage;

        public WpfRenderer(Canvas canvas, IImageProvider imageProvider)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));

            this.parentElement = this.GetTopParent();
            this.parentElement.KeyDown += HandleKeyDown;

            this.pressedKeys = new List<Key>();
            this.allowedKeys = new HashSet<Key>
            {
                Key.W,
                Key.S,
                Key.Up,
                Key.Down
            };
        }

        public override double FieldWidth => this.parentElement.Width;

        public override double FieldHeight => this.parentElement.Height;

        public override void Clear()
        {
            this.canvas.Children.Clear();
        }

        public override void DrawBall(IBall ball)
        {
            this.DrawObject(this.ballImage, BallImagePath, ball.Position.Top, ball.Position.Left, ball.Radius, ball.Radius);
        }

        public override void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer)
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
            var keysToRemove = new List<Key>();
            foreach (Key k in this.pressedKeys)
            {
                if (!Keyboard.IsKeyDown(k))
                {
                    keysToRemove.Add(k);
                }
            }

            foreach (Key k in keysToRemove)
            {
                this.pressedKeys.Remove(k);
            }

            if (!this.allowedKeys.Contains(e.Key)) return;

            if (!this.pressedKeys.Contains(e.Key))
            {
                this.pressedKeys.Add(e.Key);
            }

            foreach (var pK in this.pressedKeys)
            {
                IPlayerAction action = null;
                if (pK == Key.W)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Up);
                }
                else if (pK == Key.S)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Down);
                }
                else if (pK == Key.Up)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.SecondPlayer, Direction.Up);
                }
                else if (pK == Key.Down)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.SecondPlayer, Direction.Down);
                }
                else
                {
                    continue;
                }

                base.OnPlayerActionHappend(action);
            }

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

        public override void ShowWinner(PlayerInAction winner)
        {
            string winnerMessage = winner.ToString() + " Wins!";
            var gameEndWindow = new GameEndWindow(winnerMessage);
            this.parentElement.Close();
            //gameEndWindow.Show();
        }
    }
}
