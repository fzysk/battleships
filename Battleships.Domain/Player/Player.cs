using Battleships.Interfaces;

namespace Battleships.Domain.Player
{
    internal class Player : IPlayer
    {
        private readonly List<Ship> ships;
        private readonly IShotStrategy shotStrategy;

        public Player(List<Ship> ships, IShotStrategy shotStrategy)
        {
            this.ships = ships;
            this.shotStrategy = shotStrategy;
        }

        public IEnumerable<IGameObject> GetGameObjects() => ships.SelectMany(ship => ship.GetGameObjects());

        public (int X, int Y) MakeMove()
        {
            (int X, int Y) = shotStrategy.TakeShot();
            return (X, Y);
        }
    }
}
