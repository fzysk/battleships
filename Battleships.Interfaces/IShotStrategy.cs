using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Interfaces
{
    public interface IShotStrategy
    {       
        TakeShotResult TakeShot();
    }
}
