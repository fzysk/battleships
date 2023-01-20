using Battleships.Engine;
using Battleships.Engine.Events;
using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
using Battleships.Interfaces.Enums;
using Battleships.Interfaces.Ships;
using System.Text;

namespace Battleships.Console
{
    /// <summary>
    /// Handles writing all results to console.
    /// It presents player board on left and CPU on right
    /// </summary>
    internal class ConsoleUIWrapper
    {
        private const char BoardSeparator = '\t';
        private const char ColumnSeparator = '|';

        private readonly Game game;
        private readonly List<IPlayer> players;
        private readonly GameParameters gameParameters;

        private readonly List<ReceiveShotEvent> playerReceiveShotEvents = new List<ReceiveShotEvent>();
        private readonly List<ReceiveShotEvent> cpuReceiveShotEvents = new List<ReceiveShotEvent>();

        public ConsoleUIWrapper(Game game, List<IPlayer> players, GameParameters gameParameters)
        {
            this.game = game;
            this.players = players;
            this.gameParameters = gameParameters;

            game.ReceiveShotEvent += HandleShotFired;
        }

        public void Play()
        {
            // show starting board
            RefreshView();

            // start the game
            game.Loop();

            // game ends here, show the winner
            bool isHumanWinner = players.First(player => !player.HasLost).IsHuman;

            System.Console.WriteLine();
            System.Console.WriteLine("The game ends!");
            System.Console.WriteLine($"The winner is {(isHumanWinner ? "player" : "CPU")}!");
        }

        private void HandleShotFired(object? sender, ReceiveShotEvent e)
        {
            int sleepTime;

            if (e.ShootingPlayer.IsHuman)
            {
                cpuReceiveShotEvents.Add(e);
                sleepTime = 2000;   // more time after shot for human to catch results
            }
            else
            {
                playerReceiveShotEvents.Add(e);
                sleepTime = 1000;
            }

            RefreshView();
            
            System.Console.WriteLine();
            System.Console.WriteLine($"{(e.ShootingPlayer.IsHuman ? "Player" : "CPU")} chose {ConsoleCoordinatesAdapter.ToConsoleCoordinates(e.Result.X, e.Result.Y)}.");
            System.Console.WriteLine($"This resulted in {e.Result.ShotResult}{(e.Result.ShotResult != Interfaces.Enums.ReceiveShotEnum.Miss ? $" of {e.Result.ShipName}" : string.Empty)}");

            Thread.Sleep(sleepTime);
        }

        private void RefreshView()
        {
            System.Console.Clear();

            PrintHeaderRow();
            for (int i = 0; i < gameParameters.BoardSize; i++)
            {
                PrintSingleRow(i);
            }
        }

        private void PrintHeaderRow()
        {
            string columns = string.Join(ColumnSeparator, Enumerable.Range(1, gameParameters.BoardSize));
            System.Console.WriteLine($"  {columns}{BoardSeparator}{columns}");
        }

        private void PrintSingleRow(int row)
        {
            var sb = new StringBuilder();
            sb.Append($"{(char)('A' + row)} "); // show row letter on left

            // draw player
            var human = players.First(p => p.IsHuman);
            var objects = human.GetGameObjects();

            for (int column = 0; column < gameParameters.BoardSize; column++)
            {
                var shipPart = objects.FirstOrDefault(obj => obj.X == row && obj.Y == column);
                var shotEvent = playerReceiveShotEvents.FirstOrDefault(e => e.Result.X == row && e.Result.Y == column);

                if (shipPart != null && shipPart is IShipStatus shipStatus)
                {
                    switch (shipStatus.Status)
                    {
                        case ShipPartStatus.Healthy:
                            sb.Append('O');
                            break;
                        case ShipPartStatus.Hit:
                            sb.Append('X');
                            break;
                    }
                }
                else if (shotEvent != null)
                {
                    sb.Append('*');
                }
                else
                {
                    sb.Append('.');
                }

                sb.Append(ColumnSeparator);
            }

            sb.Append(BoardSeparator);

            // draw cpu board
            for (int column = 0; column < gameParameters.BoardSize; column++)
            {
                var shotEvent = cpuReceiveShotEvents.FirstOrDefault(e => e.Result.X == row && e.Result.Y == column);

                if (shotEvent != null)
                {
                    switch (shotEvent.Result.ShotResult)
                    {
                        case ReceiveShotEnum.Miss:
                            sb.Append('*');
                            break;
                        case ReceiveShotEnum.Hit:
                            sb.Append('X');
                            break;
                        case ReceiveShotEnum.Sunk:
                            sb.Append('!');
                            break;
                    }
                }
                else
                {
                    sb.Append('.');
                }

                sb.Append(ColumnSeparator);
            }

            System.Console.WriteLine(sb.ToString());
        }
    }
}
