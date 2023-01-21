using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.Ships;
using Battleships.Interfaces.Ships.Factories;

namespace Battleships.Console.Player
{
    internal static class ShipsPlacer
    {
        public static IEnumerable<IShip> GetShips(GameParameters gameParameters, IShipFactory shipFactory)
        {
            var ships = new List<IShip>();

            GetShips(gameParameters.BattleshipsCount, shipFactory.BuildBattleship(), ships);
            GetShips(gameParameters.DestroyersCount, shipFactory.BuildBattleship(), ships);

            return ships;
        }

        private static void GetShips(int shipCount, ISpecificShipFactory specificShipFactory, List<IShip> alreadyCreatedShips)
        {
            System.Console.WriteLine($"Please place your {specificShipFactory.ShipName.ToLower()}s: ");
            for (int i = 0; i < shipCount; i++)
            {
                System.Console.WriteLine($"({i + 1} out of {shipCount}) Please insert ships coordinates (e.g. 'A1,A2,A3,A4,A5'): ");
                
                IShip? ship = null;
                while (ship is null)
                {
                    string input = System.Console.ReadLine();

                    if (!string.IsNullOrEmpty(input))
                    {
                        System.Console.WriteLine("Please, write some coordinates here:");
                        continue;
                    }

                    try
                    {
                        foreach (var coordinates in input.Split(',', StringSplitOptions.RemoveEmptyEntries))
                        {
                            (int x, int y) = ConsoleCoordinatesAdapter.FromConsoleCoordinates(coordinates);
                            specificShipFactory.OnCoordinates(x, y);
                        }

                        ship = specificShipFactory.Create();
                    }
                    catch (Exception ex) when (ex is ArgumentException or ArgumentNullException or CoordinatesCastException)
                    {
                        System.Console.WriteLine("Please, write valid coordinates:");
                        continue;
                    }

                    if (ship.IsIntersectingWithOtherShips(alreadyCreatedShips))
                    {
                        System.Console.WriteLine("Ship is intersecting with other ships, please write valid coordinates");
                        ship = null;
                    }
                }

                alreadyCreatedShips.Add(ship);
            }
        }
    }
}
