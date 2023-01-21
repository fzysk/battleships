namespace Battleships.Interfaces.Ships.Factories
{
    public interface ISpecificShipFactory
    {
        string ShipName { get; }

        ISpecificShipFactory OnCoordinates(int x, int y);
        IShip Create();
    }
}
