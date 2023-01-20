using Battleships.Interfaces.Enums;
using Battleships.Interfaces.Ships;

namespace Battleships.Domain.Ships
{
    internal class ShipPart : GameObject, IHittable, IShipStatus
    {
        internal ShipPart(int x, int y) : base(x, y)
        {
        }

        public ShipPartStatus Status { get; private set; } = ShipPartStatus.Healthy;

        public void Hit() => Status = ShipPartStatus.Hit;
    }
}
