using System;
using System.Collections.Generic;
using System.Linq;

namespace Jk.Fullo.WordsHelper
{
    public class WordsManager : IWordsManager
    {
        private readonly IEnumerable<IWordsProvider> _providers;

        public WordsManager(IEnumerable<IWordsProvider> providers)
        {
            _providers = providers;
        }

        public IWordsStorage Get(DateTime? start, DateTime? end)
        {
            var pairs = new List<Pair>();
            foreach (var wordsProvider in _providers)
                pairs.AddRange(wordsProvider.Words(start, end));
            var words = Convert(pairs).ToList();
            return new WordsStorage(words);
        }

        private IEnumerable<Word> Convert(IEnumerable<Pair> pairs)
        {
            foreach (var pair in pairs)
            {
                var word = new Word();
                if (LanguageDetector.GetLanguage(pair.Word1) == "en")
                {
                    word.Timestamp = pair.Timestamp;
                    word.English = pair.Word1;
                    word.Russian = pair.Word2;
                }
                else
                {
                    word.Timestamp = pair.Timestamp;
                    word.English = pair.Word2;
                    word.Russian = pair.Word1;
                }
                yield return word;
            }
        }
    }
}