using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.Ships;
using Battleships.Interfaces.Ships.Factories;

namespace Battleships.Engine.Ship
{
    /// <summary>
    /// Generates all ships for CPU in random, not intersecting positions.
    /// </summary>
    public class ShipGenerator
    {
        private readonly IShipFactory shipFactory;
        private readonly IRandomGenerator generator;
        private readonly GameParameters gameParameters;

        public ShipGenerator(GameParameters gameParameters, IRandomGenerator generator, IShipFactory shipFactory)
        {
            this.gameParameters = gameParameters;
            this.generator = generator;
            this.shipFactory = shipFactory;
        }

        public IEnumerable<IShip> Generate()
        {
            // enchancement: get rid of magic numbers
            var battleships = GenerateShips(gameParameters.BattleshipsCount, 5, shipFactory.BuildBattleship());
            var destroyers = GenerateShips(gameParameters.DestroyersCount, 4, shipFactory.BuildDestroyer());

            return battleships.Concat(destroyers);
        }

        private IEnumerable<IShip> GenerateShips(int count, int shipPartsCount, ISpecificShipFactory factory) 
        {
            var ships = new List<IShip>();

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    int x = generator.Next(gameParameters.BoardSize);
                    int y = generator.Next(gameParameters.BoardSize);

                    bool horizontal = generator.Next() % 2 == 0;

                    IEnumerable<int> nextValues = GenerateShipLineCoordinates(horizontal ? x : y)
                        .Take(shipPartsCount)
                        .OrderBy(i => i);

                    foreach (var value in nextValues)
                    {
                        factory.OnCoordinates(horizontal ? x : value, horizontal ? y : value);
                    }

                    var ship = factory.Create();

                    if (!IsShipIntersecting(ship, ships))
                    {
                        ships.Add(ship);
                        break;
                    } 
                }
            }

            return ships;
        }

        private bool IsShipIntersecting(IShip ship, List<IShip> ships)
        {
            return ship.GetGameObjects().Any(part => 
                ships.SelectMany(s => s.GetGameObjects()).Any(otherPart => otherPart.X == part.X && otherPart.Y == part.Y));
        }

        /// <summary>
        /// Generates board long coordinates, starting from <paramref name="value"/>, then by it's nearest neighbours and so on.
        /// </summary>
        private IEnumerable<int> GenerateShipLineCoordinates(int value)
        {
            yield return value;

            while (true)
            {
                if (value - 1 >= 0) yield return value - 1;
                else if (value + 1 < gameParameters.BoardSize) yield return value + 1;
                else break;
            }
        }
    }
}
