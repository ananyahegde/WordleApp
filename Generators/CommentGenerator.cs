// Feedback generator
// Maintains a dictionary of mapping between number of guesses and a feedback message. 
// Returns feedback based on number of guesses.

namespace Wordle
{
    internal class CommentGenerator
    {
        static private Dictionary<int, string> _feedbackMap = new Dictionary<int, string>();

        public CommentGenerator()
        {
            _feedbackMap.Add(1, "Genius!");
            _feedbackMap.Add(2, "Excellent!");
            _feedbackMap.Add(3, "Great job!");
            _feedbackMap.Add(4, "Good work!");
            _feedbackMap.Add(5, "Nice try!");
            _feedbackMap.Add(6, "That was close!");
        }

        internal string GetComment(int guesses)
        {
            return _feedbackMap[guesses];
        }
    }
}
