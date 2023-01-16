using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.DTOs.ShotStrategy;
using System.Text.RegularExpressions;

namespace Battleships.Console.Player
{
    internal class PlayerShotStrategy : IShotStrategy
    {
        private static readonly Regex shotRegex = new Regex("([A-Z]+)([0-9]+)");

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

            var match = shotRegex.Match(input);

            if (!match.Success)
            {
                return false;
            }

            string letter = match.Captures.First().Value;
            y = int.Parse(match.Captures.Last().Value);

            if (y >= gameParameters.BoardSize)
            {
                return false;
            }

            // get letter coordinates
            for (int i = letter.Length - 1; i >= 0; i--)
            {
                x += letter[i] - 'A' + (('Z' - 'A') * i);
            }

            if (x >= gameParameters.BoardSize)
            {
                return false;
            }

            return true;
        }
    }
}
