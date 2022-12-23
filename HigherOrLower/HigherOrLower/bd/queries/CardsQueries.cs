using WebApplication1.Models;

namespace WebApplication1.bd.queries
{
    public static class CardsQueries
    {
        private const string cardsFile = "./bd/cards/cards.csv";
        public static void InsertFirst()
        {
            using (StreamWriter file = new StreamWriter(cardsFile, true))
            {
                file.WriteLine("Card Drawn;Game Name;Round");
            }
        }
        public static void InsertOne(Cards card)
        {
            FileExistes();
            using (StreamWriter file = new StreamWriter(cardsFile, true))
            {
                file.WriteLine($"{card.CardDrawn};{card.GameName};{card.GameRound}");
            }
        }
        public static Cards GetByRoundAndGame(string round, string game)
        {
            FileExistes();
            int count = 0;
            Cards result = new Cards();
            using (StreamReader file = new StreamReader(cardsFile))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[2] == round && line[1] == game)
                        {
                            result = new Cards(line[0], line[1], line[2]);
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
            return result;
        }
        private static void FileExistes()
        {
            if (!System.IO.File.Exists(cardsFile))
            {
                InsertFirst();
            }
        }
    }
}
