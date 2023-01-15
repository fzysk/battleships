using Battleships.Domain;
using Battleships.Interfaces.DTOs.Player;
using Battleships.Interfaces.DTOs.ShotStrategy;

namespace Battleships.Interfaces
{
    public interface IPlayer
    {
        bool HasLost { get; }

        IEnumerable<IGameObject> GetGameObjects();
        TakeShotResult TakeShot();
        ReceiveShotResult ReceiveShot(ReceiveShotDto shotDto);
    }
}
