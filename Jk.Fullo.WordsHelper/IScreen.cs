using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public interface IScreen
    {
        Task<bool> Print(Word fullWord, string language);
    }
}