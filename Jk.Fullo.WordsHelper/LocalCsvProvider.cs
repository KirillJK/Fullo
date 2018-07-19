using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Jk.Fullo.WordsHelper
{
    public class LocalCsvProvider : IWordsProvider
    {
        private readonly LocalCsvProviderConfiguration _configuration;

        public LocalCsvProvider(LocalCsvProviderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Pair> Words(DateTime? start, DateTime? end)
        {
            start = start ?? DateTime.MinValue;
            end = end ?? DateTime.MaxValue;
            foreach (var file in Directory.EnumerateFiles(_configuration.Root, _configuration.SearchPattern,
                _configuration.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                var info = new FileInfo(file);
                var directoryName = info.Directory.Name;
                DateTime date;
                bool selected = true;
                if (DateTime.TryParseExact(directoryName, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None,
                    out date))
                {
                    selected = start < date && end > date;
                }
                if (info.Length > 0 && selected)
                {
                    var linesFromFile = File.ReadAllLines(file, Encoding.UTF8);
                    foreach (var line in linesFromFile)
                    {
                        Pair pair;
                        if (TryParse(line, info.CreationTime, out pair)) yield return pair;
                    }
                }
            }
        }

        private bool TryParse(string line, DateTime? timestamp, out Pair pair)
        {
            pair = null;
            if (string.IsNullOrEmpty(line)) return false;
            var parts = line.Split(_configuration.Delimeters.ToCharArray());
            if (parts.Length != 2) return false;
            pair = new Pair
            {
                Word1 = parts[0].Trim(),
                Word2 = parts[1].Trim(),
                Timestamp = timestamp
            };
            return true;
        }
    }
}