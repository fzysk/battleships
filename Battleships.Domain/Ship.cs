using Battleships.Domain.GameObjects;

namespace Battleships.Domain
{
    public abstract class Ship
    {
        protected Ship(int size, ShipPart[] shipParts)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Ship cannot have size less than 1", nameof(size));
            }

            if (shipParts is null)
            {
                throw new ArgumentNullException(nameof(shipParts));
            }

            if (shipParts.Length <= 0 && shipParts.Length != size)
            {
                throw new ArgumentException($"Invalid ship parts count ({shipParts.Length})");
            } 

            Size = size;
            ShipParts = shipParts;
        }

        public int Size { get; }
        protected ShipPart[] ShipParts { get; }

        public bool IsSunk => ShipParts.All(part => part.Status == Enums.ShipPartStatus.Hit);

        public IEnumerable<IGameObject> GetGameObjects() => ShipParts;
    }
}
