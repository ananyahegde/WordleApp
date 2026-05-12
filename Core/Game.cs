namespace Wordle
{
    internal class Game
    {
        string target;
        int guessCount = 1;
        string? guessedWord;
        List<string> allGuessedWords;

        WordProvider wordProvider;
        GuessValidator guessValidator;
        FeedbackGenerator feedbackGenerator;
        CommentGenerator commentGenerator;
        LoginHandler login;
        SignupHandler signup;
        SessionHandler session;
        User? currentUser;
        StatsHandler stats;

        public Game()
        {
            wordProvider = new WordProvider();
            target = wordProvider.GetRandomWord();

            allGuessedWords = new List<string>();
            guessValidator = new GuessValidator();
            feedbackGenerator = new FeedbackGenerator();
            commentGenerator = new CommentGenerator();
            login = new LoginHandler();
            signup = new SignupHandler();
            session = new SessionHandler();
            stats = new StatsHandler();
        }

        private int CalculateScore(int guessCount)
        {
            return (7 - guessCount) * 100;
        }

        internal void Play()
        {
            Console.WriteLine("\nGame Started.\n");

            // sign up or sign in
            Console.WriteLine("Sign Up or Login to your Account");

            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Log in");
            Console.Write("\nChoose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    signup.SignupUser();
                    Console.WriteLine("\nNow please log in.");
                    currentUser = login.LoginUser();
                    break;
                case "2":
                    currentUser = login.LoginUser();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose 1 or 2.\n");
                    break;
            }

            while (true)
            {
                Console.Write("\nguess the word: ");
                guessedWord = Console.ReadLine();
                guessedWord = guessedWord.Trim().ToLower();

                if (allGuessedWords.Contains(guessedWord))
                {
                    Console.WriteLine("\nyou already guessed this word.");
                    continue;
                }

                try
                {
                    guessValidator.ValidateInput(guessedWord);
                    char[] feedback = feedbackGenerator.GetFeedback(target, guessedWord);

                    if (feedback.All(c => c == 'G'))
                    {
                        string comment = commentGenerator.GetComment(guessCount);
                        Console.Write($"\nCorrect! The word was '{target}'. ");
                        Console.WriteLine(comment);

                        // save current session
                        session.SaveSession(currentUser.userid, CalculateScore(guessCount));
                        break;
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Guess {guessCount}:   {String.Join("  ", guessedWord.ToUpper().ToCharArray())}");

                        Console.Write("Feedback:  ");
                        foreach (char c in feedback)
                        {
                            if (c == 'G') Console.ForegroundColor = ConsoleColor.Green;
                            else if (c == 'Y') Console.ForegroundColor = ConsoleColor.Yellow;
                            else Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write(c + "  ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();

                        allGuessedWords.Add(guessedWord);
                        guessCount++;
                    }
                    if (guessCount > 6)
                    {
                        bool flag = true;
                        while (flag)
                        {
                            Console.WriteLine("\nOops! You are out of guesses. What do you wanna do now?");
                            Console.WriteLine("\n1. Continue Present Game");
                            Console.WriteLine("2. Reveal Word");
                            Console.WriteLine("3. Exit");
                            Console.Write("\nChoose an option: ");
                            string? c = Console.ReadLine();

                            switch (c)
                            {
                                case "1":
                                    guessCount = 1;
                                    flag = false;
                                    break;
                                case "2":
                                    Console.WriteLine($"\nThe word was: {target.ToUpper()}");
                                    session.SaveSession(currentUser.userid, CalculateScore(guessCount));
                                    Console.WriteLine($"Your score for this round was: {CalculateScore(guessCount)}");
                                    Console.WriteLine($"Your total score: {stats.GetTotalScore(currentUser.userid)}");
                                    return;
                                case "3":
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid option. Please choose 1, 2 or 3.");
                                    break;
                            }
                        }
                    }
                }
                catch (InvalidGuessException ige)
                {
                    Console.WriteLine(ige.Message);
                }

            }
        }
    }
}
