namespace PingPong.Logic.GameObjects.Directions
{
    public class DownLeft : IDirection
    {
        public int DeltaY => 1;

        public int DeltaX => -1;
    }
}
