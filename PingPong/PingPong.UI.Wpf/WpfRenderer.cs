using PingPong.Logic.Renderers;
using System;
using System.Collections.Generic;
using PingPong.Logic.GameObjects.Contracts;
using System.Windows.Controls;
using PingPong.UI.Wpf.Helpers;
using System.Windows;
using System.Windows.Media;

namespace PingPong.UI.Wpf
{
    public class WpfRenderer : IRenderer
    {
        //private const string BallImagePath = "../../Images/ball.png";
        private const string BallImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Images\ball.png";
        private const string PlayerImagePath = @"D:\PingPongGame\PingPong\PingPong.UI.Wpf\Images\player.png";

        private Image ballImage;
        private Image firstPlImage;
        private Image secondPlImage;
        private Canvas canvas;
        private IImageProvider imageProvider;


        public WpfRenderer(Canvas canvas, IImageProvider imageProvider)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));

            this.FieldWidth = this.GetTopParent().Width;
            this.FieldHeight = this.GetTopParent().Height;
        }

        public double FieldWidth { get; }

        public double FieldHeight { get; }

        public void Clear()
        {
            this.canvas.Children.Clear();
        }

        public void DrawBall(IBall ball)
        {
            this.DrawImage(this.ballImage, BallImagePath, ball.Position.Top, ball.Position.Left, ball.Radius, ball.Radius);
        }

        public void DrawPlayers(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            this.DrawImage(this.firstPlImage, PlayerImagePath, firstPlayer.Position.Top, firstPlayer.Position.Left, firstPlayer.Size.Width, firstPlayer.Size.Height);

            this.DrawImage(this.secondPlImage, PlayerImagePath, secondPlayer.Position.Top, secondPlayer.Position.Left, secondPlayer.Size.Width, secondPlayer.Size.Height);
        }

        private void DrawImage(UIElement element, string imagePath, double topPosition, double leftPosition, double width, double height)
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

        private Window GetTopParent()
        {
            var parent = VisualTreeHelper.GetParent(this.canvas);
            while (!(parent is Window))
            {
                parent = LogicalTreeHelper.GetParent(parent);

            }

            return parent as Window;
        }
    }
}
