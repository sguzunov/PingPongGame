using PingPong.Logic.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.Logic.GameObjects.Contracts;
using System.Windows.Controls;

namespace PingPong.UI.Wpf
{
    public class WpfRenderer : IRenderer
    {
        private readonly Canvas context;

        public WpfRenderer(Canvas context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public double FieldWidth => this.context.ActualWidth;

        public double FieldHeight => this.context.ActualHeight;

        public void DrawBall(IBall ball)
        {
            throw new NotImplementedException();
        }

        public void DrawPlayers(IEnumerable<IPlayer> players)
        {
            throw new NotImplementedException();
        }
    }
}
