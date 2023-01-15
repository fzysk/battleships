using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Domain.ShotStrategies
{
    internal class RandomShotStrategy : ShotStrategy
    {
        private IRandomGenerator randomGenerator;

        public RandomShotStrategy(IRandomGenerator randomGenerator, GameParameters gameParameters) : base(gameParameters)
        {
            this.randomGenerator = randomGenerator;
        }

        public override ShotResult TakeShot() => new ShotResult { X = randomGenerator.Next(GameParameters.BoardSize), Y = randomGenerator.Next(GameParameters.BoardSize) };
    }
}
