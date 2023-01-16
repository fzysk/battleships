using Battleships.Interfaces.Enums;

namespace Battleships.Interfaces.DTOs.Player
{
    public class ReceiveShotResult
    {
        public int X { get; init; }
        public int Y { get; init; }
        public ReceiveShotEnum ShotResult { get; init; }
        public string ShipName { get; init; }
    }
}
