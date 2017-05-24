namespace PingPong.Logic.Engines
{
    public interface IGame
    {
        void InitGame(double ballVerticalStep, double ballHorizontalStep, double playerStep);

        void LoopGame();
    }
}
