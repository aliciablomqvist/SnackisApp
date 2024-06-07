namespace SnackisApp.Helpers
{
    public static class WordFilter
    {
        // List of inappropriate words to be filtered
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

        // Method to filter inappropriate words in a given input string
        public static string FilterInappropriateWords(string input)
        {
            foreach (var word in InappropriateWords)
            {
                // Create a replacement string with asterisks
                string replacement = new string('*', word.Length);
                input = System.Text.RegularExpressions.Regex.Replace(input, word, replacement, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            return input;
        }
    }
}
