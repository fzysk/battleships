using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Console.Player
{
    internal class PlayerShotStrategy : IShotStrategy
    {
        private readonly GameParameters gameParameters;

        public PlayerShotStrategy(GameParameters gameParameters)
        {
            this.gameParameters = gameParameters;
        }

        public TakeShotResult TakeShot()
        {
            System.Console.WriteLine("Please write shot coordinates (e.g. A1): ");
            string input = System.Console.ReadLine();

            int x, y;
            while (!IsInputValid(input, out x, out y))
            {
                System.Console.WriteLine("Invalid shot coordinates, please try again.");
                input = System.Console.ReadLine();
            }

            return new TakeShotResult { X = x, Y = y };
        }

        private bool IsInputValid(string? input, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (input is null)
            {
                return false;
            }

            try
            {
                (x, y) = ConsoleCoordinatesAdapter.FromConsoleCoordinates(input);
            }
            catch (CoordinatesCastException cce)
            {
                return false;
            }

            if (y >= gameParameters.BoardSize)
            {
                return false;
            }

            if (x >= gameParameters.BoardSize)
            {
                return false;
            }

            return true;
        }
    }
}
