namespace Battleships.Interfaces.DTOs.ShotStrategy
{
    public class TakeShotResult
    {
        public int X { get; init; }
        public int Y { get; init; }

        public void Deconstruct(out int X, out int Y)
        {
            X = this.X;
            Y = this.Y;
        }
    }
}
