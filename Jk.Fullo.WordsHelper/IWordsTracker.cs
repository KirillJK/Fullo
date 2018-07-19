namespace Jk.Fullo.WordsHelper
{
    public interface IWordsTracker
    {
        Word Next(Word[] words);
        string NextLanguage();
    }
}