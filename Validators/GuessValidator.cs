// Input Validator class
// Takes in a value and validates if input is a valid 5 letter word. throws `InvalidGuessException` exception otherwise.

using System.Text.RegularExpressions;

namespace Wordle
{
    internal class GuessValidator
    {
        internal void ValidateInput(string? guessedWord)
        {
            if (guessedWord.Length == 0)
            {
                throw new InvalidGuessException("Input is empty. Please give a valid 5 letter word.");
            }

            if (guessedWord.Length != 5)
            {
                throw new InvalidGuessException("Input is not a five letter word. Please give a valid 5 letter word.");
            }

            if (!Regex.IsMatch(guessedWord, @"^[a-zA-Z]+$"))
            {
                throw new InvalidGuessException("Input contains numbers or special characters. Please give a valid 5 letter word.");
            }
        }
    }
}
