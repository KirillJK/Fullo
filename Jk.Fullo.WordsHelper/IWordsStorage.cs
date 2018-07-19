using System;
using System.Collections.Generic;

namespace Jk.Fullo.WordsHelper
{
    public interface IWordsStorage
    {
        IEnumerable<Word> GetByPeriod(DateTime? start = null, DateTime? end = null);
    }
}