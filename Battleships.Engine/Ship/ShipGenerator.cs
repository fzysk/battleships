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
            var ships = new List<IShip>();

            // enchancement: get rid of magic numbers
            GenerateShips(gameParameters.BattleshipsCount, 5, shipFactory.BuildBattleship(), ships);
            GenerateShips(gameParameters.DestroyersCount, 4, shipFactory.BuildDestroyer(), ships);

            return ships;
        }

        private void GenerateShips(int count, int shipPartsCount, ISpecificShipFactory factory, List<IShip> alreadyCreatedShips) 
        {
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

                    if (!ship.IsIntersectingWithOtherShips(alreadyCreatedShips))
                    {
                        alreadyCreatedShips.Add(ship);
                        break;
                    } 
                }
            }
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
