﻿using Battleships.Domain.GameObjects;
using Battleships.Engine;
using Battleships.Engine.Events;
using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;
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
            RefreshView();
            game.Loop();
        }

        private void HandleShotFired(object? sender, ReceiveShotEvent e)
        {
            if (e.ShootingPlayer.IsHuman)
            {
                playerReceiveShotEvents.Add(e);
            }
            else
            {
                cpuReceiveShotEvents.Add(e);
            }

            RefreshView();

            System.Console.WriteLine();
            System.Console.WriteLine($"{(e.ShootingPlayer.IsHuman ? "Player" : "CPU")} chose {ConsoleCoordinatesAdapter.ToConsoleCoordinates(e.Result.X, e.Result.Y)}.");
            System.Console.WriteLine($"This resulted in {e.Result.ShotResult}{(e.Result.ShotResult != Interfaces.Enums.ReceiveShotEnum.Miss ? $"of {e.Result.ShipName}" : string.Empty)}");
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
            string columns = string.Join(ColumnSeparator, Enumerable.Range(0, gameParameters.BoardSize).Select(i => (char)('A' + i)));
            System.Console.WriteLine($"  {columns}{BoardSeparator}{columns}");
        }

        private void PrintSingleRow(int row)
        {
            var sb = new StringBuilder();
            sb.Append($"{row} "); // show row number on left

            // draw player
            var human = players.First(p => p.IsHuman);
            var objects = human.GetGameObjects();

            for (int column = 0; column < gameParameters.BoardSize; column++)
            {
                var shipPart = objects.FirstOrDefault(obj => obj.X == row && obj.Y == column);
                var shotEvent = playerReceiveShotEvents.FirstOrDefault(e => e.Result.X == row && e.Result.Y == column);

                if (shipPart != null && shipPart is ShipPart sp)
                {
                    switch (sp.Status)
                    {
                        case Domain.Enums.ShipPartStatus.Healthy:
                            sb.Append('O');
                            break;
                        case Domain.Enums.ShipPartStatus.Hit:
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

            // draw cpu
            var cpuPlayer = players.First(p => !p.IsHuman);

            for (int column = 0; column < gameParameters.BoardSize; column++)
            {
                var shotEvent = cpuReceiveShotEvents.FirstOrDefault(e => e.Result.X == row && e.Result.Y == column);

                if (shotEvent != null)
                {
                    switch (shotEvent.Result.ShotResult)
                    {
                        case Interfaces.Enums.ReceiveShotEnum.Miss:
                            sb.Append('*');
                            break;
                        case Interfaces.Enums.ReceiveShotEnum.Hit:
                            sb.Append('X');
                            break;
                        case Interfaces.Enums.ReceiveShotEnum.Sunk:
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
