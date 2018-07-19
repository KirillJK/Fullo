using System;

namespace Jk.Fullo.WordsHelper
{
    public class RandomWordsTracker: IWordsTracker
    {
        private Word _current;
        private Random _random;
        public Word Next(Word[] words)
        {
            _random = new Random(Environment.TickCount);
            _current = words[_random.Next(words.Length - 1)];
            return _current;
        }

        public string NextLanguage()
        {
            var dict = _current.GetLanguages();
            if (dict.Count == 0) throw new Exception("Empty word");
            return dict[_random.Next(0, dict.Count)];
        }
    }
}