using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Interfaces
{
    public interface IShotStrategy
    {       
        ShotResult TakeShot();
    }
}
