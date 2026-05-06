// custom exception class
// handles empty input, not a five letter word, and not a valid word(contains numbers or special characters)

namespace Wordle
{
    internal class InvalidGuessException : Exception
    {
        private string _message;
        public InvalidGuessException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}
