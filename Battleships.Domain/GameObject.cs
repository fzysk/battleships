namespace Battleships.Domain
{
    public class GameObject : IGameObject
    {
        public GameObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}