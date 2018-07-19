using System.Text.RegularExpressions;

namespace Jk.Fullo.WordsHelper
{
    public class LanguageDetector
    {
        public static string GetLanguage(string word)
        {
            if (Regex.IsMatch(word, @"\p{IsCyrillic}"))
                return "ru";
            return "en";
        }
    }
}