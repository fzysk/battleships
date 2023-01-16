using Battleships.Domain.GameObjects;
using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Player;
using Battleships.Interfaces.DTOs.ShotStrategy;
using Battleships.Interfaces.Enums;

namespace Battleships.Domain.Player
{
    public class Player : IPlayer
    {
        private readonly List<Ship> ships;
        private readonly IShotStrategy shotStrategy;

        public Player(List<Ship> ships, IShotStrategy shotStrategy, bool isHuman)
        {
            this.ships = ships;
            this.shotStrategy = shotStrategy;
            
            IsHuman = isHuman;
        }

        public bool IsHuman { get; }
        
        public bool HasLost => ships.All(ship => ship.IsSunk);
        public IEnumerable<IGameObject> GetGameObjects() => ships.SelectMany(ship => ship.GetGameObjects());

        public ReceiveShotResult ReceiveShot(ReceiveShotDto shotDto)
        {
            // try to find ship part with shot coordinates
            var shipPart = ships.SelectMany(ship => ship.GetGameObjects()).FirstOrDefault(gameObject => gameObject.X == shotDto.X && gameObject.Y == shotDto.Y);

            if (shipPart != null && shipPart is ShipPart sp)
            {
                // take shot
                sp.Hit();

                // check if ship is sunk
                var ship = ships.First(ship => ship.GetGameObjects().Contains(shipPart));

                ReceiveShotEnum shotStatus;

                if (ship.IsSunk)
                {
                    shotStatus = ReceiveShotEnum.Sunk;
                }
                else
                {
                    shotStatus = ReceiveShotEnum.Sunk;
                }

                return new ReceiveShotResult { ShipName = ship.ToString(), ShotResult = shotStatus , X = shotDto.X, Y = shotDto.Y };
            }

            return new ReceiveShotResult { ShotResult = ReceiveShotEnum.Miss, X = shotDto.X, Y = shotDto.Y };
        }

        public TakeShotResult TakeShot()
        {
            return shotStrategy.TakeShot();
        }
    }
}
