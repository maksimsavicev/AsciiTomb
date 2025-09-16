using System.Collections.Generic;

namespace AsciiTomb
{
    public class LevelManager
    {
        private List<char[,]> levels;

        public LevelManager(List<char[,]> levels)
        {
            this.levels = levels;
        }

        public char[,] CloneLevel(int index)
        {
            // make copy of level so original stays unchanged
            int rows = levels[index].GetLength(0);
            int cols = levels[index].GetLength(1);
            char[,] copy = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copy[i, j] = levels[index][i, j];
                }
            }
            return copy;
        }

        public int TotalLevels => levels.Count;
    }
}