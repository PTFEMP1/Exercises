namespace WebApplication1.bd.queries
{
    public static class PlayersQueries
    {
        private const string userFile = "./bd/user/user.csv";
        public static string[] GetById(string id)
        {
            int count = 0;
            string[] result = new string[2];
            using (StreamReader file = new StreamReader(userFile))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[0] == id)
                        {
                            result[0] = line[0];
                            result[1] = line[1];
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

        public static string[] GetByName(string name)
        {
            int count = 0;
            string[] result = new string[2];
            using (StreamReader file = new StreamReader(userFile))
            {
                while (!file.EndOfStream)
                {
                    if (count != 0)
                    {
                        string[] line = file.ReadLine().Split(";");
                        if (line[1] == name)
                        {
                            result[0] = line[0];
                            result[1] = line[1];
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
    }
}
