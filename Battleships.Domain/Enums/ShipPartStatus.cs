namespace Battleships.Domain.Enums
{
    public enum ShipPartStatus
    {
        /// <summary>
        /// This part of ship isn't hit by anyone
        /// </summary>
        Healthy,

        /// <summary>
        /// Someone has hit the ship
        /// </summary>
        Hit,
    }
}
