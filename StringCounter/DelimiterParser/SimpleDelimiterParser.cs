namespace StringCounter.DelimiterParser
{
    public class SimpleDelimiterParser : IDelimiterParser
    {
        protected readonly char[] DefaultDelimiters;

        public SimpleDelimiterParser(char[] defaultDelimiters = null)
        {
            if (defaultDelimiters == null || defaultDelimiters.Length <= 0)
            {
                DefaultDelimiters = new char[] {','};
                return;
            }

            DefaultDelimiters = defaultDelimiters;
        }

        /// <summary>
        /// Retrieve a list of defaultDelimiters that can be used to parse a string
        /// </summary>
        /// <param name="input">A string of delimited values</param>
        /// <param name="delimiters">An array of distinct defaultDelimiters that can be used to parse the input string</returns>
        public virtual string Parse(string input, out char[] delimiters)
        {
            delimiters = DefaultDelimiters;

            return input;
        }
    }
}
