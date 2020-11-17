namespace StringCounter.DelimiterParser
{
    public class MultiDelimiterParser : SimpleDelimiterParser
    {
        public MultiDelimiterParser() :base(new char[] { ',', '\n' })
        {
        }
    }
}
