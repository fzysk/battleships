namespace Battleships.Domain
{
    public class GameObject : IGameObject
    {
        internal GameObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}