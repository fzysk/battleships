using Battleships.Engine.Events;
using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Player;

namespace Battleships.Engine
{
    public class Game
    {
        private event EventHandler<ReceiveShotEvent> ReceiveShotEvent;

        private readonly IPlayer firstPlayer;
        private readonly IPlayer secondPlayer;

        public Game(IEnumerable<IPlayer> players)
        {
            if (players is null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            if (players.Count() != 2)
            {
                throw new ArgumentException("2 players are required for game to be played", nameof(players));
            }

            firstPlayer = players.First();
            secondPlayer = players.Last();
        }

        public bool HasEnded => firstPlayer.HasLost || secondPlayer.HasLost;

        public void Loop()
        {
            while (!HasEnded)
            {
                MakeMove(firstPlayer, secondPlayer);

                if (secondPlayer.HasLost)
                {
                    break;
                }

                MakeMove(secondPlayer, firstPlayer);
            }
        }

        private void MakeMove(IPlayer playerWithTurn, IPlayer playerWithoutTurn)
        {
            var shot = playerWithTurn.TakeShot();
            var receiveShotResult = playerWithoutTurn.ReceiveShot(new ReceiveShotDto { X = shot.X, Y = shot.Y });

            ReceiveShotEvent?.Invoke(this, new ReceiveShotEvent { Result = receiveShotResult, ShootingPlayer = playerWithTurn });
        }
    }
}