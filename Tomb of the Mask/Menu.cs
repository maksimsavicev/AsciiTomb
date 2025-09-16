using System;

namespace AsciiTomb
{
    public class Menu
    {
        private int maxLevelCompleted;

        public Menu(int maxLevel)
        {
            maxLevelCompleted = maxLevel;
        }

        public int ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main Menu\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ____  ____  ____ _    _____ ____  _      ____ \n" +
                                  "/  _ \\/ ___\\/   _Y \\  /__ __Y  _ \\/ \\__/|/  __\\\n" +
                                  "| / \\||    \\|  / | |    / \\ | / \\|| |\\/||| | //\n" +
                                  "| |-||\\___ ||  \\_| |    | | | \\_/|| |  ||| |_\\\\\n" +
                                  "\\_/ \\|\\____/\\____|_/    \\_/ \\____/\\_/  \\|\\____/\n\n");
                Console.ResetColor();
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Level Selector");
                Console.WriteLine("3. Quit Game");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                    return 1; // start game
                if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) 
                    return 2; // level selector
                if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3) 
                    return 3; // quit
            }
        }

        public int LevelSelector()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Level Selector\n");

                for (int i = 0; i < Levels.AllLevels.Count; i++)
                {
                    if (maxLevelCompleted >= i)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write("┌   ┐ ");
                }
                Console.WriteLine();

                for (int i = 0; i < Levels.AllLevels.Count; i++)
                {
                    if (maxLevelCompleted >= i)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write($"  {i + 1}   ");
                }
                Console.WriteLine();

                for (int i = 0; i < Levels.AllLevels.Count; i++)
                {
                    if (maxLevelCompleted >= i)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write("└   ┘ ");
                }

                Console.ResetColor();
                Console.WriteLine("\n\nWrite which level you want to start:");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int selectedLevel))
                {
                    selectedLevel -= 1;

                    // allow only levels that are already unlocked
                    if (selectedLevel >= 0 && selectedLevel <= maxLevelCompleted)
                        return selectedLevel;
                }

                Console.WriteLine("Invalid choice. Press any key to try again.");
                Console.ReadKey(true);
            }
        }
    }
}