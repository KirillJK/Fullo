using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public class Lcd1602Screen : IScreen
    {
        private static SerialPort _serialPort;

        public Lcd1602Screen()
        {
            var names = SerialPort.GetPortNames();
            var firstPort = names.LastOrDefault();
            _serialPort = new SerialPort(firstPort);
            _serialPort.Open();
        }

        public async Task<bool> Print(Word fullWord, string language)
        {
            var word = fullWord.GetWordByLanguage(language);
            var message = Convert(word).ToArray();
            _serialPort.Write(message, 0, message.Count());
            return false;
        }

        private static void FillPage(Dictionary<char, byte> dict, string vector, byte elder)
        {
            byte counter = 0;
            foreach (var item in vector)
            {
                dict[item] = (byte)(elder | counter);
                counter++;
            }
        }

        private static Dictionary<char, byte> Registry()
        {
            Dictionary<char, byte> results = new Dictionary<char, byte>();
            FillPage(results, "БГЁЖЗИЙЛПУФЧШЪЫЭ", 0xA0);
            FillPage(results, "ЮЯбвгёжзийклмнпт", 0xB0);
            FillPage(results, "чшъыьэюя", 0xC0);
            FillPage(results, "_____Х", 0xD0);
            FillPage(results, "ДЦЩдфцщ_________", 0xE0);
            FillPage(results, "р_______ху", 0x70);
            FillPage(results, "_АВС_Е__Н__К_М_О", 0x40);
            FillPage(results, "Р___Т___Х", 0x50);
            FillPage(results, "PQRSTUVWXYZ[|]^_", 0x50);
            FillPage(results, "'abcdefghijklmno", 0x60);
            FillPage(results, "pqrstuvwxyz", 0x70);
            FillPage(results, "_а_с_е_________о", 0x60);
            FillPage(results, "0123456789:;<=>?", 0x30);
            FillPage(results, "@ABCDEFGHIJKLMNO", 0x40);
            results['Ь'] = 0xC4;

            return results;
        }

        private static IEnumerable<byte> Convert(string message)
        {
            var registry = Registry();
            foreach (var item in message)
            {
                if (registry.ContainsKey(item))
                {
                    yield return registry[item];
                }
                else
                {
                    yield return 0x20;
                }
            }
        }
    }
}