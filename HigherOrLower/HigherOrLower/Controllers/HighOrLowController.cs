using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using System.Xml.Linq;
using WebApplication1.Models;
using WebApplication1.bd.queries;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighOrLowController : ControllerBase
    {
        private readonly ILogger<HighOrLowController> _logger;
        private const string userFile = "./bd/user/user.csv";
        private const string gamesFile = "./bd/games/games.csv";

        public HighOrLowController(ILogger<HighOrLowController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Bem vindo, para te registares vai na seguinte pagina: \n https://localhost:7053/HighOrLow/Registar/{O Teu Nome}"
                + " GetGameList/{O Teu userName}/Lobby";
        }
        [HttpGet]
        [Route("GetGameList/{user}/Lobby")]
        public string GameList(string user)
        {
            return user;
        }
        [HttpGet]
        [Route("Registar/{user}")]
        public string Registo(string user)
        {
            try
            {
                FileExistes(userFile);
                if (ExistsUser(user))
                {
                    return "User already Exists";
                }
                Player player = createPlayer(user);
                AddUser(player);

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }

        [HttpGet]
        [Route("CreateGame/{user}/{gameName}")]
        public string CreateGame(string user, string gameName)
        {
            try
            {
                if (!ExistsUser(user))
                {
                    return "User " + user + " Doesn't Exists";
                }
                Player player = new Player(PlayersQueries.GetByName(user));
                List<Player> players = new List<Player>();
                List<Score> scores = new List<Score>();
                players.Add(player);
                Game game = new Game(Guid.NewGuid().ToString(), gameName, player, players.ToArray(), scores.ToArray(), "Waiting Players", DateTime.Now, 0);
                GamesQueries.InsertOne(game);
                return "Criado";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }

        [HttpGet]
        [Route("Play/{user}/{gameName}/")]
        public string Play(string user, string gameName)
        {
            try
            {
                if (!ExistsUser(user))
                {
                    return "User " + user + " Doesn't Exists";
                }
                if (!ExistsGame(gameName))
                {
                    return "Game " + gameName + " Doesn't Exists";
                }

                Game game = GamesQueries.GetByName(gameName);
                if (game.status == "Waiting Players")
                {
                    return "Need more players";
                }
                if (game.status == "Ready")
                {
                    Random rnd = new Random();
                    int card = rnd.Next(5, 20);
                    int cardDrawon = rnd.Next(2, 15);
                    game.cards = card;
                    game.status = "Round-1";
                    Cards cardObj = new Cards(cardDrawon.ToString(), game.Name, game.status);
                    GamesQueries.updateGame(game);
                    CardsQueries.InsertOne(cardObj);
                    return "Game Started, current Card: " + cardObj.CardDrawn;
                }
                Cards currentCard = CardsQueries.GetByRoundAndGame(game.status, game.Name);
                return "Current Card: " + currentCard.CardDrawn;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }
        [HttpGet]
        [Route("Play/{user}/{gameName}/Higher")]
        public string PlayHigher(string user, string gameName)
        {
            try
            {
                if (!ExistsUser(user))
                {
                    return "User " + user + " Doesn't Exists";
                }
                if (!ExistsGame(gameName))
                {
                    return "Game " + gameName + " Doesn't Exists";
                }

                Game game = GamesQueries.GetByName(gameName);
                if (game.status == "Waiting Players")
                {
                    return "Need more players";
                }
                if (game.status == "Ready")
                {
                    return "Game Hasnt Started";
                }
                Cards oldCard = CardsQueries.GetByRoundAndGame(game.status, game.Name);
                Random rnd = new Random();
                string currentRound = game.status;
                int cardDrawon = rnd.Next(2, 15);
                game.status = GetRoundStatus(game.status, game.cards);
                Cards currentCard = new Cards(cardDrawon.ToString(), game.Name, game.status);

                string result = string.Empty;
                string point = string.Empty;
                if (int.Parse(currentCard.CardDrawn) > int.Parse(oldCard.CardDrawn))
                {
                    point = "1";
                    result = "You Won 1 Point! Current Card: " + currentCard.CardDrawn;
                }
                else
                {
                    point = "0";
                    result = "You Lost! no Points! Current Card: " + currentCard.CardDrawn;
                }
                GamesQueries.updateGame(game);
                Score score = new Score(user, game.Name, currentRound, point);
                CardsQueries.InsertOne(currentCard);
                ScoreQueries.InsertOne(score);
                return result;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }
        [HttpGet]
        [Route("Play/{user}/{gameName}/Lower")]
        public string Lower(string user, string gameName)
        {
            try
            {
                if (!ExistsUser(user))
                {
                    return "User " + user + " Doesn't Exists";
                }
                if (!ExistsGame(gameName))
                {
                    return "Game " + gameName + " Doesn't Exists";
                }

                Game game = GamesQueries.GetByName(gameName);
                if (game.status == "Waiting Players")
                {
                    return "Need more players";
                }
                if (game.status == "Ready")
                {
                    return "Game Hasnt Started";
                }
                Cards oldCard = CardsQueries.GetByRoundAndGame(game.status, game.Name);
                Random rnd = new Random();
                string currentRound = game.status;
                int cardDrawon = rnd.Next(2, 15);
                game.status = GetRoundStatus(game.status, game.cards);
                Cards currentCard = new Cards(cardDrawon.ToString(), game.Name, game.status);

                string result = string.Empty;
                string point = string.Empty;
                if (int.Parse(currentCard.CardDrawn) < int.Parse(oldCard.CardDrawn))
                {
                    point = "1";
                    result = "You Won 1 Point! Current Card: " + currentCard.CardDrawn;
                }
                else
                {
                    point = "0";
                    result = "You Lost! no Points! Current Card: " + currentCard.CardDrawn;
                }
                GamesQueries.updateGame(game);
                Score score = new Score(user, game.Name, currentRound, point);
                CardsQueries.InsertOne(currentCard);
                ScoreQueries.InsertOne(score);
                return result;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }

        [HttpGet]
        [Route("JoinGame/{user}/{gameName}/")]
        public string JoinGame(string user, string gameName)
        {
            try
            {
                if (!ExistsUser(user))
                {
                    return "User " + user + " Doesn't Exists";
                }
                if (!ExistsGame(gameName))
                {
                    return "Game " + gameName + " Doesn't Exists";
                }
                Player player = new Player(PlayersQueries.GetByName(user));
                Game game = GamesQueries.GetByName(gameName);
                List<Player> players = game.players.ToList();
                if (players.Count() >= 10)
                {
                    return "Max players Reached";
                }
                players.Add(player);
                game.players = players.ToArray();
                GamesQueries.updateGame(game);
                return "Sucesso";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return user;
        }

        [HttpGet]
        [Route("GameList")]
        public string GameList()
        {
            try
            {
                string Lista = string.Empty;
                GamesFileExistes(gamesFile);
                List<Game> games = GetGameList();
                foreach (Game game in games)
                {
                    Lista += game.toString();
                }
                return string.IsNullOrEmpty(Lista) ? "NO Games found" : Lista;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        private List<Game> GetGameList()
        {
            List<Game> games = new List<Game>();

            foreach (string gameInfo in GamesQueries.GetAllGames())
            {
                games.Add(new Game(gameInfo.Split(";")));
            }
            return games;
        }
        private void FileExistes(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("id;name");
                }
            }
        }

        private void GamesFileExistes(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                GamesQueries.InsertFirst();
            }
        }

        private bool ExistsUser(string user)
        {
            if (string.IsNullOrEmpty(PlayersQueries.GetByName(user)[0]))
            {
                return false;
            }
            return true;
        }
        private bool ExistsGame(string gameName)
        {
            if (string.IsNullOrEmpty(GamesQueries.GetByName(gameName).Name))
            {
                return false;
            }
            return true;
        }
        private Player createPlayer(string user)
        {
            Player player = new Player(Guid.NewGuid().ToString(), user);
            return player;
        }

        private void AddUser(Player user)
        {
            using (StreamWriter file = new StreamWriter(userFile, true))
            {
                file.WriteLine(user.Id + ";" + user.Name);
            }

        }
      
        private string GetRoundStatus(string status, int limitRound)
        {
            if (status == "Ready")
            {
                return "Round-1";
            }
            else
            {
                string[] round = status.Split("-");
                int roundNumber = int.Parse(round[1]);
                if (roundNumber >= limitRound)
                {
                    return "Finished";
                }
                string newStatus = "Round-" + roundNumber+1;
                return newStatus;
            }
        }
    }
}