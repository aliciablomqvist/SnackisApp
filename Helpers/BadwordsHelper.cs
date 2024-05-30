
namespace SnackisApp.Helpers
{
    public static class WordFilter
{
    public static List<string> InappropriateWords = new List<string>
    {

        "bastard",
        "bitch",
        "crap",
        "damn",
        "douche",
        "fuck",
        "prick",
        "shit"
    };

    public static string FilterInappropriateWords(string input)
    {
        foreach (var word in InappropriateWords)
        {
            string replacement = new string('*', word.Length);
            input = System.Text.RegularExpressions.Regex.Replace(input, word, replacement, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        return input;
    }
}
}
