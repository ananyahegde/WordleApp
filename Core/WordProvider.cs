// WordProvider class.
// fetches all 5 letter words from words.txt upon object initialization.
// Provides a method GetRandomWord() method to get a random target word for the current game.
// change this ------

namespace Wordle
{
    internal class WordProvider
    {
        static private string[] _words;
        public WordProvider()
        {
            try
            {
                _words = File.ReadAllLines("words.txt");
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("`words.txt` file is not present in the project directory.");
                // Console.WriteLine(fnfe);
            }
        }

        internal string GetRandomWord()
        {
            string target = _words[new Random().Next(_words.Length)];
            return target;
        }
    }
}
