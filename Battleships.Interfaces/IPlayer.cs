using Battleships.Domain;

namespace Battleships.Interfaces
{
    public interface IPlayer
    {
        IEnumerable<IGameObject> GetGameObjects();

        (int X, int Y) MakeMove();
    }
}
