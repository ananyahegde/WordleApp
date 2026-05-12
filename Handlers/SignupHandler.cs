using Npgsql;

namespace Wordle
{
    public class SignupHandler
    {
        User user;
        Connect connect;
        NpgsqlConnection connection;

        public SignupHandler()
        {
            user = new User();
            connect = new Connect();
            connection = connect.ConnectDb();
        }

        public User CreateUser(User user)
        {
            string insertQuery = $"INSERT INTO users(username, password, createdat) VALUES ('{user.username}', '{user.password}', '{user.createdat:yyyy-MM-dd HH:mm:ss}')";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            try
            {
                connection?.Close();
                connection.Open();

                int result = command.ExecuteNonQuery();

                if (result > 0)
                    Console.WriteLine("User created successfully!");
                else
                    Console.WriteLine("Something went wrong.");
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
            return user;
        }

        public void SignupUser()
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

                    user.createdat = DateTime.UtcNow;

                    User createdUser = CreateUser(user);
                    if (createdUser != null)
                    {
                        Console.WriteLine($"Created User {createdUser.username}");
                        break;
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Invalid format!");
                }
            }
        }
    }
}
