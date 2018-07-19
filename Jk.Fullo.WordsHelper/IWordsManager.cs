using System;

namespace Jk.Fullo.WordsHelper
{
    public interface IWordsManager
    {
        IWordsStorage Get(DateTime? start, DateTime? end);
    }
}