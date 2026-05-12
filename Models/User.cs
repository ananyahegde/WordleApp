namespace Wordle
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public DateTime createdat { get; set; }
    }
}
