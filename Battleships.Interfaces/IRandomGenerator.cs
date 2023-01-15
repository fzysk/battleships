namespace Battleships.Interfaces
{
    public interface IRandomGenerator
    {
        /// <summary>
        /// Randomize number from 0 to 1
        /// </summary>
        int Next();

        /// <summary>
        /// Randomize number from 0 to max (exclusive)
        /// </summary>
        int Next(int max);
    }
}
