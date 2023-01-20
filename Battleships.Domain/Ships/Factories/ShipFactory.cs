using Battleships.Interfaces.Ships.Factories;

namespace Battleships.Domain.Ships.Factories
{
    public class ShipFactory : IShipFactory
    {
        public ISpecificShipFactory BuildBattleship()
        {
            return new BattleshipFactory();
        }

        public ISpecificShipFactory BuildDestroyer()
        {
            return new DestroyerFactory();
        }
    }
}
