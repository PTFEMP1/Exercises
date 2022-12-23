namespace WebApplication1.Models
{
    public class Score
    {
        public string PlayerName { get; set; }
        public string GameName { get; set; }
        public string Round { get; set; }
        public string Points { get; set; }
        public Score(string playerName, string gameName, string round, string score)
        {
            PlayerName = playerName;
            GameName = gameName;
            Round = round;
            Points = score;
        }

    }
}
