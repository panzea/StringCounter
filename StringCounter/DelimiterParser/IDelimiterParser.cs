namespace StringCounter.DelimiterParser
{
    public interface IDelimiterParser
    {
        string Parse(string input, out char[] delimiters);
    }
}
