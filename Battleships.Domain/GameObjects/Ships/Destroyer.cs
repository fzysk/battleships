namespace Battleships.Domain.GameObjects.Ships
{
    public class Destroyer : Ship
    {
        private const int DestroyerSize = 4;

        public Destroyer(ShipPart[] shipParts) : base(DestroyerSize, shipParts)
        {
        }

        public override string ToString() => nameof(Destroyer);
    }
}
