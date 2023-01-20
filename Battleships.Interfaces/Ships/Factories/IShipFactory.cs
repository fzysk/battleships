namespace Battleships.Interfaces.Ships.Factories
{
    public interface IShipFactory
    {
        ISpecificShipFactory BuildBattleship();
        ISpecificShipFactory BuildDestroyer();
    }
}
