using Npgsql;

namespace Wordle
{
    public class LoginHandler
    {
        User user;
        Connect connect;
        NpgsqlConnection connection;

        public LoginHandler()
        {
            user = new User();
            connect = new Connect();
            connection = connect.ConnectDb();
        }

        public User GetUser(User user)
        {
            string query = $"SELECT * FROM users WHERE username='{user.username}' AND password='{user.password}'";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);

            try
            {
                connection?.Close();
                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.userid = (int)reader[0];
                    user.username = (string)reader[1];
                    user.password = (string)reader[2];
                    user.createdat = (DateTime)reader[3];
                    return user;
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection?.Close();
            }
        }

        public User LoginUser()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter username: ");
                    string input = Console.ReadLine() ?? "";
                    if (input == "")
                    {
                        Console.WriteLine("Username cannot be empty!");
                        continue;
                    }
                    user.username = input;

                    Console.Write("Enter password: ");
                    string password = Console.ReadLine() ?? "";
                    if (password == "")
                    {
                        Console.WriteLine("Password cannot be empty!");
                        continue;
                    }
                    user.password = password;

                    User loggedInUser = GetUser(user);
                    if (loggedInUser != null)
                    {
                        Console.WriteLine($"Welcome back, {loggedInUser.username}!");
                        return loggedInUser;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid format!");
                }
            }
        }
    }
}
