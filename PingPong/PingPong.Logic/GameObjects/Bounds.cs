namespace PingPong.Logic.GameObjects
{
    public class Bounds
    {
        public Bounds()
        {
        }

        public Bounds(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
