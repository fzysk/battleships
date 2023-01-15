namespace Battleships.Domain.GameObjects.Ships
{
    internal class Battleship : Ship
    {
        internal const int BattleshipSize = 5;

        public Battleship(ShipPart[] shipParts) : base(BattleshipSize, shipParts)
        {
        }
    }
}
