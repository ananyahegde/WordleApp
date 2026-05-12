using Npgsql;
namespace Wordle
{
    public class SessionHandler
    {
        Connect connect;
        NpgsqlConnection connection;
        public SessionHandler()
        {
            connect = new Connect();
            connection = connect.ConnectDb();
        }

        public void SaveSession(int userid, int score)
        {
            string query = $"INSERT INTO sessions(userid, score, playedat) VALUES ('{userid}', '{score}', '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}')";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);

            try
            {
                connection?.Close();
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}
