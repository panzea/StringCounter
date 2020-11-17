using System.Linq;
using System.Text.RegularExpressions;

namespace StringCounter.DelimiterParser
{
    public class CustomDelimiterParser : MultiDelimiterParser
    {
        private static readonly string DelimiterPatternPrefix = "//";
        private static readonly string DelimiterPatternSuffix = "\n";

        /// <summary>
        /// Parse delimiters from a delimiter pattern at the start of the string or using the default delimiters
        /// </summary>
        /// <param name="input">A string containing a list separated by one or more delimiters which can optionally
        /// be specified at the start of the string using a delimiter definition pattern in the format
        /// //[delim1][delim2]\n[delimited string...]</param>
        /// <param name="delimiters">An array of distinct delimiters parsed from the string including the default delimiters</param>
        /// <returns>The remainder of the string after the delimiter definition pattern</returns>
        public override string Parse(string input, out char[] delimiters)
        {
            var pattern = Regex.Escape(DelimiterPatternPrefix) + "(.*?)" + Regex.Escape(DelimiterPatternSuffix) +
                          "(.*)";

            if(!Regex.IsMatch(input, pattern))
            {
                delimiters = DefaultDelimiters;
                return input;
            }

            var match = Regex.Match(input, pattern, RegexOptions.Singleline);

            delimiters = match.Groups[1].Value.ToCharArray();
            delimiters = delimiters.Concat(DefaultDelimiters).Distinct().ToArray();

            return match.Groups[2].Value;
        }
    }
}
