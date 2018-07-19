using System;

namespace Jk.Fullo.WordsHelper
{
    public class LocalConfiguration
    {
        public bool English { get; set; }
        public bool Russian { get; set; }
        public int Delay { get; set; } = 15000;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}