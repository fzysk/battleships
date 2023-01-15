namespace Battleships.Interfaces
{
    public interface IRandomGenerator
    {
        int Next();
        int Next(int max);
    }
}
