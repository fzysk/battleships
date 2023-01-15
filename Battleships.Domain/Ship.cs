using Battleships.Domain.GameObjects;

namespace Battleships.Domain
{
    internal abstract class Ship
    {
        public Ship(int size, ShipPart[] shipParts)
        {
            Size = size;
            ShipParts = shipParts;
        }

        protected int Size { get; }
        protected ShipPart[] ShipParts { get; }

        public IEnumerable<IGameObject> GetGameObjects() => ShipParts;
    }
}
