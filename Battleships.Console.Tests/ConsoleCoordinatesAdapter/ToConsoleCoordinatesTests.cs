namespace Battleships.Console.Tests.ConsoleCoordinatesAdapter
{
    public class ToConsoleCoordinatesTests
    {
        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void NegativeCoordinatesInParameters_ShouldThrowAnException(int x, int y)
        {
            // Act
            Action act = () => Console.ConsoleCoordinatesAdapter.ToConsoleCoordinates(x, y);
            
            // Assert
            act.Should().Throw<CoordinatesCastException>();
        }

        [Theory]
        [InlineData(0, 0, "A1")]
        [InlineData(0, 1, "A2")]
        [InlineData(0, 2, "A3")]
        [InlineData(1, 0, "B1")]
        [InlineData(2, 1, "C2")]
        [InlineData(3, 2, "D3")]
        [InlineData(4, 5, "E6")]
        [InlineData(5, 6, "F7")]
        [InlineData(6, 9, "G10")]
        [InlineData(23, 0, "X1")]
        [InlineData(24, 10, "Y11")]
        [InlineData(25, 12, "Z13")]
        [InlineData(26, 99, "AA100")]
        [InlineData(52, 0, "BA1")]
        [InlineData(676, 999, "AAA1000")]
        [InlineData(17576, 99999, "AAAA100000")]
        public void NonNegativeCoordinates_ShouldBeConvertedToCoordinateString(int x, int y, string expectedCoordinates)
        {
            // Act
            var result = Console.ConsoleCoordinatesAdapter.ToConsoleCoordinates(x, y);

            // Assert
            result.Should().Be(expectedCoordinates);
        }
    }
}
