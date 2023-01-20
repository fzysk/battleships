namespace Battleships.Interfaces.Ships.Factories
{
    public interface ISpecificShipFactory
    {
        ISpecificShipFactory OnCoordinates(int x, int y);
        IShip Create();
    }
}
