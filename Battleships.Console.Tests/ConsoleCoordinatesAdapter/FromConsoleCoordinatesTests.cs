namespace Battleships.Console.Tests.ConsoleCoordinatesAdapter
{
    public class FromConsoleCoordinatesTests
    {
        [Fact]
        public void NullStringCoordinates_ShouldThrowAnException()
        {
            // Arrange
            string coordinates = null;

            // Act
            Action act = () => Console.ConsoleCoordinatesAdapter.FromConsoleCoordinates(coordinates);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]  // only numbers
        [InlineData("12")]
        [InlineData("121249861268947813238721")]
        [InlineData("A")]   // only letters
        [InlineData("BAO")]
        [InlineData("BAOIOHUDSFHOIUDSAFOHIUASF")]
        [InlineData("120ASDW")] // inverted coordinates
        [InlineData("18L")]
        [InlineData("a1")]  // lowercase letters
        [InlineData("Aa234")]
        [InlineData("Ð3")]  // non-latin letters
        [InlineData("ØØ1")]
        public void InvalidStringCoordinates_ShouldThrowAnException(string coordinates)
        {
            // Act
            Action act = () => Console.ConsoleCoordinatesAdapter.FromConsoleCoordinates(coordinates);

            // Assert
            act.Should().Throw<CoordinatesCastException>();
        }

        [Theory]
        [MemberData(nameof(ValidCoordinates_Data))]
        public void ValidCoordinates_ShouldBeConvertedSuccessfully(string coordinates, int expectedX, int expectedY)
        {
            // Act
            (int x, int y) = Console.ConsoleCoordinatesAdapter.FromConsoleCoordinates(coordinates);

            // Assert
            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        public static IEnumerable<object[]> ValidCoordinates_Data()
        {
            // params: coordinates, expectedX, expectedY
            yield return new object[] { "A1", 0, 0 };
            yield return new object[] { "B2", 1, 1 };
            yield return new object[] { "G10", 6, 9 };
            yield return new object[] { "X1", 23, 1 };
            yield return new object[] { "Z13", 25, 12 };
            yield return new object[] { "AA100", 26, 99 };
            yield return new object[] { "BA1", 52, 0 };
            yield return new object[] { "AAA1000", 676, 999 };
            yield return new object[] { "AAAA100000", 17576, 99999 };
        }
    }
}
