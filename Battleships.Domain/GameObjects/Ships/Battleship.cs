namespace Battleships.Domain.GameObjects.Ships
{
    public class Battleship : Ship
    {
        private const int BattleshipSize = 5;

        public Battleship(ShipPart[] shipParts) : base(BattleshipSize, shipParts)
        {
        }

        public override string ToString() => nameof(Battleship);
    }
}
