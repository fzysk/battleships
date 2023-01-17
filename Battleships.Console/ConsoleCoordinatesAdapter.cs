using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Battleships.Console.Tests")]
namespace Battleships.Console
{
    internal static class ConsoleCoordinatesAdapter
    {
        /// <summary>
        /// Columns are like Excel columns (e.g. A,B,...,Z,AA,AB,...)
        /// </summary>
        private static readonly int CoordinatesBase = 'Z' - 'A';
        private static readonly Regex ShotRegex = new Regex("([A-Z]+)([0-9]+)");

        public static string ToConsoleCoordinates(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new CoordinatesCastException();
            }

            int multiplier = 0;
            var sb = new StringBuilder();

            do
            {
                multiplier = x / CoordinatesBase;

                int c = x - multiplier * CoordinatesBase;
                x -= multiplier * CoordinatesBase;

                sb.Append((char)(c + 'A'));
            } 
            while (multiplier != 0);

            sb.Append(y + 1);
            return sb.ToString();
        }

        public static (int x, int y) FromConsoleCoordinates(string coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            var match = ShotRegex.Match(coordinates);

            if (match.Success)
            {
                string letters = match.Groups[1].Value;
                int y = int.Parse(match.Groups[2].Value);

                int x = 0;
                // get letter coordinates
                for (int i = letters.Length - 1; i >= 0; i--)
                {
                    x += letters[i] - 'A' + (CoordinatesBase * i);
                }

                return (x, y - 1);
            }

            throw new CoordinatesCastException();
        }
    }

    internal class CoordinatesCastException : Exception
    {
    }
}
