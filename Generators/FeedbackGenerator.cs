namespace Wordle
{
    internal class FeedbackGenerator
    {
        internal char[] GetFeedback(string target, string guessedWord)
        {
            char[] feedbackArray = new char[5];
            {
                for (int i = 0; i < guessedWord.Length; i++)
                {
                    if (guessedWord[i] == target[i])
                    {
                        feedbackArray[i] = 'G';
                    }

                    else if (target.Contains(guessedWord[i]))
                    {
                        feedbackArray[i] = 'Y';
                    }

                    else
                    {
                        feedbackArray[i] = 'X';
                    }
                }
                return feedbackArray;
            }
        }
    }
}
