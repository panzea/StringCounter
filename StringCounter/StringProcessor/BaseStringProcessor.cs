using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCounter.StringProcessor
{
    /// <summary>
    /// Base class for processing a delimited string into its component parts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseStringProcessor<T> : IStringProcessor<T>
    {
        /// <summary>
        /// Given a delimited string, split it into its component parts
        /// </summary>
        /// <param name="input">A delimited string</param>
        /// <param name="delimiters">An array of delimiters used within the input string</param>
        /// <returns>An array of values parsed into a specialised format</returns>
        public virtual T[] Process(string input, char[] delimiters)
        {
            // As spec did not directly indicate what should happen with null or whitespace strings an assumption was
            // made that they should be handled the same as empty strings. Typically would have validated this assumption
            // first.
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<T>();

            // In order to support case-insensitive string split, we need to use Regex.Split over string.Split.
            // We need to prepare the regex pattern to support this:
            var regexPattern = PrepareDelimitersAsRegexPattern(delimiters);

            var splitNumbers = Regex.Split(input, regexPattern, RegexOptions.IgnoreCase);
            GuardAgainstConsecutiveDelimiters(splitNumbers);

            return splitNumbers.Select(ParseEntry).ToArray();
        }

        /// <summary>
        /// Allow derived classes to implement the parsing logic for each delimited entry within the string
        /// </summary>
        /// <param name="entry">An individual entry within the delimited string</param>
        /// <returns>The specialised form of the entry after data type conversion</returns>
        protected abstract T ParseEntry(string entry);

        protected string PrepareDelimitersAsRegexPattern(char[] delimiters)
        {
            return "(?:" + string.Join("|", delimiters.Select(d => Regex.Escape(d.ToString()))) + ")";
        }

        protected static void GuardAgainstConsecutiveDelimiters(string[] numbers)
        {
            if (numbers.Any(n => n == string.Empty))
                throw new ArgumentException("Delimiters must not be used consecutively");
        }
    }
}
