using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public interface IScreens {
        Task<bool> Print(Word fullWord, string language);
        void RegisterScreen(IScreen screen);
    }
}