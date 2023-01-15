using Battleships.Interfaces.Enums;

namespace Battleships.Interfaces.DTOs.Player
{
    public class ReceiveShotResult
    {
        public ReceiveShotEnum ShotResult { get; init; }
        public string ShipName { get; init; }
    }
}
