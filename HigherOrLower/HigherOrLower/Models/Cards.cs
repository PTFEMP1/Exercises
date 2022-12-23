namespace WebApplication1.Models
{
    public class Cards
    {
        public string CardDrawn { get; set; }
        public string GameName { get; set; }
        public string GameRound { get; set; }
        public Cards (string cardDrawn, string gameName, string gameRound )
        {
            CardDrawn = cardDrawn;
            GameName = gameName;
            GameRound = gameRound;
        }
        public Cards()
        {

        }
    }
}
