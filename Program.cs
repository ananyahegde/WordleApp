namespace Wordle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Wordle!\n");
            Console.WriteLine("How to Play:");
            Console.WriteLine("Guess the secret 5-letter word in 6 attempts or less.\n");
            Console.WriteLine("After each guess, you'll get feedback on your letters:");
            Console.WriteLine("• G = Correct letter in the correct position");
            Console.WriteLine("• Y = This letter is in the word but in the wrong position");
            Console.WriteLine("• X = This letter is not in the word at all\n");
            Console.WriteLine("Use the feedback to narrow down the word. Good luck!");

            while (true)
            {
                Console.WriteLine("\n\nWelcome back!\n");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Game game = new Game();
                        game.Play();
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1 or 2.\n");
                        break;
                }
            }
        }
    }
}
