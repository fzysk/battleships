using Battleships.Domain;
using Battleships.Domain.GameObjects;
using Battleships.Domain.GameObjects.Ships;
using Battleships.Interfaces.DTOs.Game;

namespace Battleships.Console.Player
{
    internal static class ShipsPlacer
    {
        public static List<Ship> GetShips(GameParameters gameParameters)
        {
            var ships = new List<Ship>();

            ProcessSingleShipClass<Battleship>(gameParameters.BattleshipsCount, ships);
            ProcessSingleShipClass<Destroyer>(gameParameters.DestroyersCount, ships);

            return ships;
        }

        private static void ProcessSingleShipClass<T>(int shipCount, List<Ship> ships) where T : Ship
        {
            System.Console.WriteLine($"Please place your {typeof(T).Name.ToLower()}: ");
            for (int i = 0; i < shipCount; i++)
            {
                System.Console.WriteLine($"({i + 1} out of {shipCount}) Please insert ships coordinates (e.g. 'A1,A2,A3,A4,A5'): ");
                string input = System.Console.ReadLine();

                // todo: validate user input

                var shipParts = new List<ShipPart>();
                foreach (var coordinates in input.Split(','))
                {
                    (int x, int y) = ConsoleCoordinatesAdapter.FromConsoleCoordinates(input);
                    shipParts.Add(new ShipPart(x, y));
                }

                ships.Add((T)Activator.CreateInstance(typeof(T), new object[] { shipParts.ToArray() }));
            }
        }
    }
}
