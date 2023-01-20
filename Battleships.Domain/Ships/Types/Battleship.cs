namespace Battleships.Domain.Ships.Types
{
    internal class Battleship : Ship
    {
        private const int BattleshipSize = 5;

        internal Battleship(ShipPart[] shipParts) : base(BattleshipSize, shipParts)
        {
        }

        public override string ToString() => nameof(Battleship);
    }
}
