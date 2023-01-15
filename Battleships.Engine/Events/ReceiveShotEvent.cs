using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Player;

namespace Battleships.Engine.Events
{
    public class ReceiveShotEvent
    {
        public IPlayer ShootingPlayer { get; init; }
        public ReceiveShotResult Result { get; init; }
    }
}
