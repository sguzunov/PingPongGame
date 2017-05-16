namespace PingPong.Logic.GameObjects.Contracts
{
    public interface IPlayer : IGameObject
    {
        Bounds Size { get; }
    }
}
