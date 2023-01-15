using Battleships.Domain.Enums;

namespace Battleships.Domain.GameObjects
{
    internal class ShipPart : GameObject
    {
        public ShipPart(int x, int y) : base(x, y)
        {
        }

        public ShipPartStatus Status { get; private set; } = ShipPartStatus.Healthy;

        public void Hit() => Status = ShipPartStatus.Hit;
    }
}
