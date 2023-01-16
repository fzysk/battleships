// See https://aka.ms/new-console-template for more information
using Battleships.Console;
using Battleships.Console.Player;
using Battleships.Domain;
using Battleships.Domain.GameObjects;
using Battleships.Domain.GameObjects.Ships;
using Battleships.Domain.Player;
using Battleships.Engine;
using Battleships.Engine.ShotStrategies;
using Battleships.Interfaces;
using Battleships.Interfaces.DTOs.Game;

// enchancement: store it in some app settings
var gameParameters = new GameParameters
{
    BattleshipsCount = 1,
    DestroyersCount = 2,
    BoardSize = 10,
};

// enchancement: ships factory/generator for computer player
var computerShips = new List<Ship>
{
    new Battleship(Enumerable.Range(1, 5).Select(i => new ShipPart(4, i)).ToArray()),
    new Destroyer(Enumerable.Range(2, 4).Select(i => new ShipPart(i, 5)).ToArray()),
    new Destroyer(Enumerable.Range(3, 4).Select(i => new ShipPart(i, 1)).ToArray()),
};
var computerPlayer = new Player(computerShips, new RandomShotStrategy(new RandomGenerator(), gameParameters), isHuman: false);

var playerShips = ShipsPlacer.GetShips(gameParameters);
var humanPlayer = new Player(playerShips, new PlayerShotStrategy(gameParameters), isHuman: true);

// shuffle players
var players = new List<IPlayer> { humanPlayer, computerPlayer }.OrderBy(x => new Random().Next()).ToList();

var game = new Game(players);
var consoleWrapper = new ConsoleUIWrapper(game, players, gameParameters);

consoleWrapper.Play();