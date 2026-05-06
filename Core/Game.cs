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

        public Game()
        {
            wordProvider = new WordProvider();
            target = wordProvider.GetRandomWord();

            allGuessedWords = new List<string>();
            guessValidator = new GuessValidator();
            feedbackGenerator = new FeedbackGenerator();
            commentGenerator = new CommentGenerator();
        }

        internal void Play()
        {
            Console.WriteLine("\nGame Started.\n");
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
                            string choice = Console.ReadLine();

                            switch (choice)
                            {
                                case "1":
                                    guessCount = 1;
                                    flag = false;
                                    break;
                                case "2":
                                    Console.WriteLine($"\nThe word was: {target.ToUpper()}");
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
