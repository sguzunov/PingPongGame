namespace PingPong.Logic.GameObjects.Directions
{
    public class UpRight : IDirection
    {
        public int DeltaY => -1;

        public int DeltaX => 1;
    }
}
