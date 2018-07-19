using System;
using System.Collections.Generic;

namespace Jk.Fullo.WordsHelper
{
    public interface IWordsProvider
    {
        IEnumerable<Pair> Words(DateTime? start = null, DateTime? end = null);
    }
}