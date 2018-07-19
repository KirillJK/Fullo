using System.Threading;
using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public class Asker: IAsker
    {
        private IWordsTracker _wordsTracker;
        private IScreens _screens;
        static CancellationTokenSource _source = new CancellationTokenSource();
        public Asker(IWordsTracker wordsTracker, IScreens screens)
        {
            _wordsTracker = wordsTracker;
            _screens = screens;
        }

        public async Task StartAsking(Word[] words, LocalConfiguration configuration)
        {
            var token = _source.Token;
            var nextWord = _wordsTracker.Next(words);
            var nextLanguage = _wordsTracker.NextLanguage();
            while (true)
            {
                await _screens.Print(nextWord, nextLanguage);
                nextWord = _wordsTracker.Next(words);
                nextLanguage = _wordsTracker.NextLanguage();
                await Task.Delay(configuration.Delay, token);
            }
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        public void Renew()
        {
            _source = new CancellationTokenSource();
        }
    }
}