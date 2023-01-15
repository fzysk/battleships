using Battleships.Interfaces;

namespace Battleships.Engine
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random random = new Random();

        public int Next() => random.Next();
        public int Next(int max) => random.Next(max);
    }
}
