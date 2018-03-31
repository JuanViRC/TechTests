using System;

namespace TechTest
{
    public class Grid : IGrid
    {
        private int[,] matrix;

        public int LengthX { get => matrix.GetLength(0); }
        public int LengthY { get => matrix.GetLength(1); }

        public Grid(int lengthX, int lengthY)
        {
            if (lengthX < 0) throw new ArgumentNullException();
            if (lengthY < 0) throw new ArgumentNullException();

            matrix = new int[lengthX, lengthY];
        }
    }
}