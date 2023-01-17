using Battleships.Domain.Enums;
using Battleships.Interfaces;

namespace Battleships.Domain.GameObjects
{
    // only public to create ships from console projects
    // a ship factory should be created and then we could hide this (plus specific ships classes)
    public class ShipPart : GameObject, IHittable
    {
        public ShipPart(int x, int y) : base(x, y)
        {
        }

        public ShipPartStatus Status { get; private set; } = ShipPartStatus.Healthy;

        public void Hit() => Status = ShipPartStatus.Hit;
    }
}
