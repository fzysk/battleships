using Battleships.Domain;

namespace Battleships.Interfaces.Ships
{
    public interface IShip
    {
        int Size { get; }
        bool IsSunk { get; }

        IEnumerable<IGameObject> GetGameObjects();
        bool IsIntersectingWithOtherShips(List<IShip> ships);
    }
}
