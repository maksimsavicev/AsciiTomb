using System;
using System.Threading.Tasks;

namespace AsciiTomb
{
    public class Game
    {
        public int PlayerX 
        { 
            get; 
            private set; 
        }
        public int PlayerY 
        { 
            get; 
            private set; 
        }
        public int CurrentLevel 
        { 
            get; 
            private set; 
        }
        public char[,] Map 
        { 
            get; 
            private set; 
        }

        private LevelManager levelManager;

        public Game(LevelManager manager)
        {
            levelManager = manager;
        }

        public void LoadLevel(int levelIndex)
        {
            CurrentLevel = levelIndex;
            Map = levelManager.CloneLevel(levelIndex);
            FindPlayerPos();
        }

        private void FindPlayerPos()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == 'P') 
                    { 
                        PlayerX = i; 
                        PlayerY = j; 
                        return; 
                    }
                }
            }
        }

        public void Move(int dx, int dy)
        {
            // movement animation
            while (PlayerX + dx >= 0 && PlayerX + dx < Map.GetLength(0) && PlayerY + dy >= 0 && PlayerY + dy < Map.GetLength(1) && Map[PlayerX + dx, PlayerY + dy] == '-')
            {
                Map[PlayerX, PlayerY] = '+';
                PlayerX += dx;
                PlayerY += dy;
                Map[PlayerX, PlayerY] = 'P';

                DrawMap();
                Task.Delay(75).Wait();
            }
        }

        public bool CheckComplete()
        {
            for (int i = 0; i < Map.GetLength(0); i++) 
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == '-') 
                        return false;
                }
            }
            return true;
        }

        public bool NextLevel()
        {
            CurrentLevel++;
            if (CurrentLevel < levelManager.TotalLevels)
            {
                LoadLevel(CurrentLevel);
                return true;
            }
            return false;
        }

        public void DrawMap()
        {
            Console.Clear();
            Console.WriteLine("Level " + (CurrentLevel + 1) + "\n");

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    ColorPicker(Map[i, y]);
                    Console.Write("┌   ┐ ");
                }
                Console.WriteLine();

                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    ColorPicker(Map[i, y]);
                    Console.Write("      ");
                }
                Console.WriteLine();

                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    ColorPicker(Map[i, y]);
                    Console.Write("└   ┘ ");
                }
                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine("\n\n\u001b[3mUse W, A, S, D to move. Press R to Restart. Press Q to return to Main Menu.\u001b[0m");
        }

        private void ColorPicker(char symbol)
        {
            switch (symbol)
            {
                case '-': 
                    Console.ForegroundColor = ConsoleColor.DarkGray; 
                    break;
                case 'P': 
                    Console.ForegroundColor = ConsoleColor.DarkBlue; 
                    break;
                case '*': 
                    Console.ForegroundColor = ConsoleColor.Red; 
                    break;
                case '+': 
                    Console.ForegroundColor = ConsoleColor.Cyan; 
                    break;
            }
        }
    }
}