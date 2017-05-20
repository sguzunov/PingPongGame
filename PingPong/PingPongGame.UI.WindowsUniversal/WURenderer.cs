using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using PingPong.Logic.Command;
using PingPong.Logic.Enums;
using PingPong.Logic.GameObjects.Contracts;
using PingPong.Logic.Renderers;
using PingPongGame.UI.WindowsUniversal.Providers;

namespace PingPongGame.UI.WindowsUniversal
{
    public class WURenderer : BaseGameRenderer, IRenderer
    {
        private const string BallImagePath = @"Assets/Game/ball.png";
        private const string PlayerImagePath = @"Assets/Game/player.png";

        private readonly Canvas canvas;
        private readonly IImageProvider imageProvider;
        private readonly ICollection<VirtualKey> pressedKeys;
        private readonly ICollection<VirtualKey> allowedKeys;

        private Image ballImage;
        private Image firstPlImage;
        private Image secondPlImage;

        public WURenderer(Canvas canvas, IImageProvider imageProvider, double fieldWidth, double fieldHeight)
            : base()
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));
            this.FieldWidth = fieldWidth;
            this.FieldHeight = fieldHeight;

            Window.Current.CoreWindow.KeyDown += HandleKeysDown;

            this.pressedKeys = new List<VirtualKey>();
            this.allowedKeys = new HashSet<VirtualKey>
            {
                VirtualKey.W,
                VirtualKey.S,
                VirtualKey.Up,
                VirtualKey.Down
            };
        }

        public override double FieldWidth { get; }

        public override double FieldHeight { get; }

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

        public override void ShowWinner(PlayerInAction winner)
        {
            throw new NotImplementedException();
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

        private void HandleKeysDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            var keysToRemove = new List<VirtualKey>();
            foreach (VirtualKey k in this.pressedKeys)
            {
                var keyState = Window.Current.CoreWindow.GetKeyState(k);
                if (!keyState.HasFlag(CoreVirtualKeyStates.Down))
                {
                    keysToRemove.Add(k);
                }
            }

            foreach (VirtualKey k in keysToRemove)
            {
                this.pressedKeys.Remove(k);
            }

            if (!this.allowedKeys.Contains(args.VirtualKey)) return;

            if (!this.pressedKeys.Contains(args.VirtualKey))
            {
                this.pressedKeys.Add(args.VirtualKey);
            }

            foreach (var pK in this.pressedKeys)
            {
                IPlayerAction action = null;
                if (pK == VirtualKey.W)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Up);
                }
                else if (pK == VirtualKey.S)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.FirstPlayer, Direction.Down);
                }
                else if (pK == VirtualKey.Up)
                {
                    action = PlayerAction.CreatePlayerAction(PlayerInAction.SecondPlayer, Direction.Up);
                }
                else if (pK == VirtualKey.Down)
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
    }
}
