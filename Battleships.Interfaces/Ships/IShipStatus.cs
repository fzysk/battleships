using Battleships.Interfaces.Enums;

namespace Battleships.Interfaces.Ships
{
    public interface IShipStatus
    {
        ShipPartStatus Status { get; }
    }
}
