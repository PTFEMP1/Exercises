using WebApplication1.Models;

namespace WebApplication1.bd.queries
{
    public static class ScoreQueries
    {
        private const string filePath = "./bd/score/score.csv";
        public static void InsertOne(Score score)
        {
            FileExistes();
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine($"{score.PlayerName};{score.GameName};{score.Round};{score.Points}");
            }
        }
        public static void InsertFirst()
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("Player Name;Game Name;Round;Points");
            }
        }
        public static List<Score> GetAllRoundsByGameName(string name)
        {
            FileExistes();
            int count = 0;
            List<Score> result = new List<Score> ();
            using (StreamReader file = new StreamReader(filePath))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[0] == name)
                        {
                            result.Add(new Score(line[0], line[1], line[2], line[3]));
                        }
                    }
                    else
                    {
                        _ = file.ReadLine();
                    }
                    count++;
                }
            }
            return result;
        }
        public static List<Score> GetAllRoundsByGameNameAndRound(string name, string round)
        {
            FileExistes();
            int count = 0;
            List<Score> result = new List<Score>();
            using (StreamReader file = new StreamReader(filePath))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[0] == name && line[2] == round)
                        {
                            result.Add(new Score(line[0], line[1], line[2], line[3]));
                        }
                    }
                    else
                    {
                        _ = file.ReadLine();
                    }
                    count++;
                }
            }
            return result;
        }
        private static void FileExistes()
        {
            if (!System.IO.File.Exists(filePath))
            {
                InsertFirst();
            }
        }
    }
}
