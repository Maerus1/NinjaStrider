using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStrider
{
    class HighScoreManager
    {
        public const string PATH = "highscores.txt";
        public static void saveScore()
        {
            if (Shared.gameOver && !Shared.activatedGodMode)
            {
                if (!File.Exists(PATH))
                {
                    using (StreamWriter writer = File.CreateText(PATH))
                    {
                        writer.WriteLine(Shared.score.ToString());
                    }
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(PATH))
                    {
                        writer.WriteLine(Shared.score.ToString());
                    }
                }

            }
        }
        public static List<int> loadScores()
        {
            if (File.Exists(PATH))
            {
                string[] arr = File.ReadAllLines("highscores.txt");
                List<string> tempList = new List<string>(arr);

                try
                {
                    return tempList.Select(int.Parse).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Data corruption! Error: " + ex.GetBaseException().Message);
                    Console.WriteLine("The game will now erase all the data");
                    using (StreamWriter writer = File.CreateText(PATH))
                    {
                        writer.Write(String.Empty);
                    }
                    Shared.systemMessage = "The high score file has been corrupted.\nErasing data...";
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
