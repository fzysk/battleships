namespace Battleships.Domain.Ships.Types
{
    internal class Destroyer : Ship
    {
        private const int DestroyerSize = 4;

        internal Destroyer(ShipPart[] shipParts) : base(DestroyerSize, shipParts)
        {
        }

        public override string ToString() => nameof(Destroyer);
    }
}
