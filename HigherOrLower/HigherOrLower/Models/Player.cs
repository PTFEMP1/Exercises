using WebApplication1.bd.queries;

namespace WebApplication1.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Player(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public Player(string id)
        {
            string[] result = PlayersQueries.GetById(id);
            Map(result);
        }
        public Player(string[] info)
        {
            Map(info);
        }
        private void Map(string[] result)
        {
            Id = result[0];
            Name = result[1];
        }
    }
}
