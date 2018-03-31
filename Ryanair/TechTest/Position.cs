namespace TechTest
{
    public class Position : IPosition
    {
        public int X { get; private set; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}