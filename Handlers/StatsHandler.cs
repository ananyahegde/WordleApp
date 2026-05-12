using Npgsql;
namespace Wordle
{
    public class StatsHandler
    {
        Connect connect;
        NpgsqlConnection connection;

        public StatsHandler()
        {
            connect = new Connect();
            connection = connect.ConnectDb();
        }

        public long GetTotalScore(int userid)
        {
            string query = $"SELECT SUM(score) FROM sessions WHERE userid='{userid}'";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);

            try
            {
                connection?.Close();
                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return (long)reader[0];
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}
