using Battleships.Console;
using Battleships.Console.Player;
using Battleships.Domain.Players;
using Battleships.Domain.Ships.Factories;
using Battleships.Engine;
using Battleships.Engine.Ship;
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

var randomGenerator = new RandomGenerator();
var shipFactory = new ShipFactory();

var shipGenerator = new ShipGenerator(gameParameters, randomGenerator, shipFactory);
var computerPlayer = new Player(shipGenerator.Generate().ToList(), new RandomShotStrategy(new RandomGenerator(), gameParameters), isHuman: false);

var playerShips = ShipsPlacer.GetShips(gameParameters, shipFactory);
var humanPlayer = new Player(playerShips.ToList(), new PlayerShotStrategy(gameParameters), isHuman: true);

// shuffle players
var players = new List<IPlayer> { humanPlayer, computerPlayer }.OrderBy(x => new Random().Next()).ToList();

var game = new Game(players);
var consoleWrapper = new ConsoleUIWrapper(game, players, gameParameters);

consoleWrapper.Play();