namespace Jk.Fullo.WordsHelper
{
    public class LocalCsvProviderConfiguration
    {
        public string SearchPattern { get; set; }
        public string Root { get; set; }
        public bool Recursive { get; set; }
        public string Delimeters { get; set; } = ",";
    }
}