using Battleships.Domain.Enums;
using Battleships.Interfaces;

namespace Battleships.Domain.GameObjects
{
    internal class ShipPart : GameObject, IHittable
    {
        public ShipPart(int x, int y) : base(x, y)
        {
        }

        public ShipPartStatus Status { get; private set; } = ShipPartStatus.Healthy;

        public void Hit() => Status = ShipPartStatus.Hit;
    }
}
