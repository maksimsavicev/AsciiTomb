using System.IO;

namespace AsciiTomb
{
    public class SaveManager
    {
        private string filePath;

        public SaveManager(string path)
        {
            filePath = path;
        }

        public int LoadProgress()
        {
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                if (int.TryParse(text, out int number))
                    return number; // return saved level
            }

            return 0; // no save found
        }

        public void SaveProgress(int level)
        {
            File.WriteAllText(filePath, level.ToString()); // overwrite progress
        }
    }
}