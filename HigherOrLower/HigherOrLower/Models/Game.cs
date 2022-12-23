namespace WebApplication1.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Player owner { get; set; }
        public Player[] players { get; set; }
        public Score[] scores { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; }
        public int cards { get; set; }
        public Game()
        {

        }
        public Game(string id, string name, Player owner, Player[] players, Score[] scores, string status, DateTime createDate,int cards)
        {
            Id = id;
            Name = name;
            this.owner = owner;
            this.players = players;
            this.scores = scores;
            this.status = status;
            this.createDate = createDate;
            this.cards = cards; 
        }

        public Game(string[] properties)
        {
            //("id;name;owner;players;scores;status;createDate;cards");
            Id = properties[0];
            Name = properties[1];
            owner = new Player(properties[2]);
            players = getListPlayer(properties[3]);
            scores = getListScores(properties[4]);
            status = properties[5];
            createDate = DateTime.Parse(properties[6]);
            cards = int.Parse(properties[7]);
        }
        public string toString()
        {
            return "Game of: " + owner.Name + ", Number of Players: " + players.Count() + " Data de criação: " + createDate + "\n";
        }
        private Player[] getListPlayer(string info)
        {
            List<Player> list = new List<Player>();
            string[] ids = info.Split(",");
            foreach (string id in ids)
            {
                list.Add(new Player(id));
            }
            return list.ToArray();
        }
        private Score[] getListScores(string info)
        {
            List<Score> list = new List<Score>();
            if (string.IsNullOrEmpty(info))
            {
                return list.ToArray();
            }
            return list.ToArray();
        }
        
    }
}
