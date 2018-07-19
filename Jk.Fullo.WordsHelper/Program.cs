using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jk.Fullo.WordsHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = JsonConvert.DeserializeObject<FullConfiguration>(File.ReadAllText("config.json"));
            IWordsProvider wordsProvider = new LocalCsvProvider(configuration.CsvProvider);
            IWordsManager manager = new WordsManager(new List<IWordsProvider>() {wordsProvider});
            var storage = manager.Get(configuration.Words.Start, configuration.Words.End);
            var words = storage.GetByPeriod().ToList();
            PrintHeader(configuration.Words.Delay);
            Screens screens = new Screens();
            screens.RegisterScreen(new ConsoleScreen());
            screens.RegisterScreen(new Lcd1602Screen());
            IAsker asker = new Asker(new RandomWordsTracker(), screens);
            Task.Run(() =>
            {
                while (true)
                {
                    var r = Console.ReadKey(true);
                    if (r.Key == ConsoleKey.Spacebar)
                    {
                        asker.Cancel();
                    }
                    if (r.Key == ConsoleKey.OemPlus)
                    {
                        int value = configuration.Words.Delay;
                        Interlocked.Add(ref value, + 1000);
                        configuration.Words.Delay = value;
                        PrintHeader(configuration.Words.Delay);
                        asker.Cancel();
                    }
                    if (r.Key == ConsoleKey.OemMinus)
                    {
                        if (configuration.Words.Delay >= 2000)
                        {
                            int value = configuration.Words.Delay;
                            Interlocked.Add(ref value, - 1000);
                            configuration.Words.Delay = value;
                            PrintHeader(configuration.Words.Delay);
                            asker.Cancel();
                        }
                    }
                }
            });
            while (true)
            {
                try
                {
                    asker.Renew();
                    asker.StartAsking(words.ToArray(), configuration.Words).Wait();
                }
                catch (Exception e)
                {

                }
            }

        }

        private static void PrintHeader(int speed)
        {
            Console.Title = $"Current speed = {speed / 1000}  sec / word";
        }

    }
}