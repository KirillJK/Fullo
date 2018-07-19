using System.Threading;
using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public interface IAsker
    {
        Task StartAsking(Word[] words, LocalConfiguration configuration);
        void Cancel();
        void Renew();
    }
}