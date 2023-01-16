using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Engine
{
    public abstract class ShotStrategy : IShotStrategy
    {
        protected ShotStrategy(GameParameters gameParameters)
        {
            GameParameters = gameParameters;
        }

        protected GameParameters GameParameters { get; }

        public abstract TakeShotResult TakeShot();
    }
}
