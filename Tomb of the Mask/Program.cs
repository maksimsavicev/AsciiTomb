using System;
using AsciiTomb;

class Program
{
    static void Main()
    {
        var levelManager = new LevelManager(Levels.AllLevels);
        var saveManager = new SaveManager("progress.txt");
        int maxLevelCompleted = saveManager.LoadProgress();

        while (true) // main loop
        {
            var menu = new Menu(maxLevelCompleted);
            int choice = menu.ShowMainMenu();
            int currentLevel = 0;

            if (choice == 2)
                currentLevel = menu.LevelSelector(); // level selector
            else if (choice == 3)
                return; // game quit

            var game = new Game(levelManager);
            game.LoadLevel(currentLevel);

            bool backToMenu = false;

            while (!backToMenu) // game loop
            {
                game.DrawMap();

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        game.Move(-1, 0);
                        break;
                    case ConsoleKey.S:
                        game.Move(1, 0);
                        break;
                    case ConsoleKey.A:
                        game.Move(0, -1);
                        break;
                    case ConsoleKey.D:
                        game.Move(0, 1);
                        break;
                    case ConsoleKey.R:
                        game.LoadLevel(game.CurrentLevel);
                        break;
                    case ConsoleKey.Q:
                        backToMenu = true;
                        break;
                }

                if (backToMenu) break;

                if (game.CheckComplete())
                {
                    if (maxLevelCompleted < game.CurrentLevel)
                    {
                        maxLevelCompleted = game.CurrentLevel;
                        saveManager.SaveProgress(maxLevelCompleted + 1);
                    }

                    if (!game.NextLevel())
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n\nCongratulations! You completed all levels!");
                        Console.ResetColor();
                        Console.WriteLine("\nPress any key to return to Main Menu...");
                        Console.ReadKey(true);
                        backToMenu = true;
                    }
                }
            }
        }
    }
}