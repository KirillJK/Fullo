using System;
using System.Collections.Generic;
using System.Linq;

namespace Jk.Fullo.WordsHelper
{
    public class WordsStorage : IWordsStorage
    {
        private readonly List<Word> _words;

        public WordsStorage(List<Word> words)
        {
            _words = words;
        }

        public IEnumerable<Word> GetByPeriod(DateTime? start, DateTime? end)
        {
            start = start ?? DateTime.MinValue;
            end = end ?? DateTime.MaxValue;
            return _words.Where(a => (a.Timestamp ?? DateTime.MinValue.AddMilliseconds(1)) < end &&
                                     (a.Timestamp ?? DateTime.MinValue.AddMilliseconds(1)) > start);
        }
    }
}