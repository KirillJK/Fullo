using System;
using System.Collections.Generic;

namespace Jk.Fullo.WordsHelper
{
    public class Word
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public DateTime? Timestamp { get; set; }

        public Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            if (English != null) languages[0] = nameof(English);
            if (Russian != null) languages[1] = nameof(Russian);
            return languages;
        }
        public string GetWordByLanguage(string language)
        {
            if (language.ToLower() == nameof(English).ToLower())
            {
                return English;
            }
            return Russian;
        }
    }
}