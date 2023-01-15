namespace Battleships.Domain.GameObjects.Ships
{
    internal class Destroyer : Ship
    {
        internal const int DestroyerSize = 4;

        public Destroyer(ShipPart[] shipParts) : base(DestroyerSize, shipParts)
        {
        }
    }
}
