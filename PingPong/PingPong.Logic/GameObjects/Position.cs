namespace PingPong.Logic.GameObjects
{
    public class Position
    {
        public Position()
        {
        }

        public Position(double top, double left)
        {
            this.Top = top;
            this.Left = left;
        }

        public double Top { get; set; }

        public double Left { get; set; }
    }
}
