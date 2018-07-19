using System;
using System.Text;
using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public class ConsoleScreen:IScreen
    {
        public ConsoleScreen()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public async Task<bool> Print(Word fullWord, string language)
        {
            Console.WriteLine(fullWord.Russian + ","+fullWord.English);
            return true;
        }
    }
}