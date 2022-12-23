using System.Collections.Generic;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.bd.queries
{
    public static class GamesQueries
    {
        private const string filePath = "./bd/games/games.csv";

        public static Game GetByName(string name)
        {
            int count = 0;
            Game game = new Game();
            using (StreamReader file = new StreamReader(filePath))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[1] == name)
                        {
                            game = new Game(line);
                            break;
                        }
                    }
                    else
                    {
                        _ = file.ReadLine();
                    }
                    count++;
                }
            }
            return game;
        }
        public static List<string> GetAllGames()
        {
            int count = 0;
            List<string> result = new List<string>();
            using (StreamReader file = new StreamReader(filePath))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        result.Add(file.ReadLine());
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
        
        public static void InsertOne(Game game)
        {
            string playersIds = string.Empty;
            foreach (Player player in game.players)
            {
                if (!string.IsNullOrEmpty(playersIds))
                {
                    playersIds += ",";
                }
                playersIds += player.Id;
            }
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine($"{game.Id};{game.Name};{game.owner.Id};{playersIds};;{game.status};{game.createDate.ToString()};{game.cards}");
            }
        }
        public static void InsertFirst()
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("id;name;owner;players;scores;status;createDate;cards");
            }
        }
        public static void updateGame(Game updated)
        {
            var lines = File.ReadAllLines(filePath);
            var linesToWrite = lines.ToList();
            foreach (var s in lines)
            {
                var split = s.Split(';');
                if (split[1] == updated.Name)
                {
                    linesToWrite.Remove(s);
                }

            }

            File.WriteAllLines(filePath, linesToWrite.ToArray());
            InsertOne(updated);
        }
    }
}
