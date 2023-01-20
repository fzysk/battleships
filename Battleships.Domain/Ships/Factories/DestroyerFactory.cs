using Battleships.Domain.Ships.Types;
using Battleships.Interfaces.Ships;
using Battleships.Interfaces.Ships.Factories;

namespace Battleships.Domain.Ships.Factories
{
    internal class DestroyerFactory : ISpecificShipFactory
    {
        private readonly List<ShipPart> parts = new List<ShipPart>();

        public IShip Create()
        {
            return new Destroyer(parts.ToArray());
        }

        public ISpecificShipFactory OnCoordinates(int x, int y)
        {
            parts.Add(new ShipPart(x, y));
            return this;
        }
    }
}
