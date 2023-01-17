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
        private static readonly int CoordinatesBase = 'Z' - 'A' + 1;
        private static readonly Regex ShotRegex = new Regex("([A-Z]+)([0-9]+)");

        public static string ToConsoleCoordinates(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new CoordinatesCastException();
            }

            bool isAfterFirstLoop = false;

            var sb = new StringBuilder();
            var stack = new Stack<char>();

            do
            {
                int lastDigit = x % CoordinatesBase;

                if (x < CoordinatesBase && isAfterFirstLoop)
                {
                    // check for powers of coordinates base
                    // at last loop (x < coordinates base) we end up with x == 1
                    // however, 1 is B in our notation, but we expect it to be A
                    // and we don't want to do it for x = [0-25] (hence isAfterFirstLoop flag)
                    lastDigit--;
                }

                // move to next "number"
                x /= CoordinatesBase;

                stack.Push((char)(lastDigit + 'A'));

                isAfterFirstLoop = true;
            }
            while (x > 0);

            sb.AppendJoin("", stack);
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
