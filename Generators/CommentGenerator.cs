// Feedback generator
// Maintains a dictionary of mapping between number of guesses and a feedback message. 
// Returns feedback based on number of guesses.

namespace Wordle
{
    internal class CommentGenerator
    {
        static private Dictionary<int, string> _feedbackMap = new Dictionary<int, string>()
            {
                { 1, "Genius!" },
                { 2, "Excellent!" },
                { 3, "Great job!" },
                { 4, "Good work!" },
                { 5, "Nice try!" },
                { 6, "That was close!" }
            };

        internal string GetComment(int guesses)
        {
            return _feedbackMap[guesses];
        }
    }
}
