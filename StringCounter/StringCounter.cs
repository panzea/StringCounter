using System;
using System.Linq;
using StringCounter.DelimiterParser;
using StringCounter.StringProcessor;

namespace StringCounter
{
    /// <remarks>
    /// I took the challenge to represent a single set of specifications, and thus following a TDD
    /// approach I created my tests and coded to "fix" the tests, refactoring my codebase as each new
    /// test was introduced to match the specification. Another approach would have been to follow the open-closed
    /// principle of SOLID and start with a StringCounter class and extend that class with each new implementation
    /// variant. These variants would have been introduced inline with the specification as it became known. This
    /// approach would lend itself when the requirements are evolving but code is already published
    /// and in use (as would be typical in the development of an evolving system).
    /// </remarks>
    public class StringCounter
    {
        private readonly IDelimiterParser _delimiterParser;
        private readonly IStringProcessor<int> _stringToNumberProcessor;

        public StringCounter(IDelimiterParser delimiterParser, IStringProcessor<int> stringToNumberProcessor)
        {
            _delimiterParser = delimiterParser;
            _stringToNumberProcessor = stringToNumberProcessor;
        }

        /// <summary>
        /// Sums the values of a string of numbers separated by delimiters
        /// </summary>
        /// <param name="input">A list of numbers separated by one or more delimiters which can optionally
        /// be specified at the start of the string using the format //[delim1][delim2]\n[numbers...]</param>
        /// <returns></returns>
        public int Add(string input)
        {
            // As spec did not directly indicate what should happen with null or whitespace strings an assumption was
            // made that they should be handled the same as empty strings. Typically would have validated this assumption
            // first.
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var delimitedNumbers = _delimiterParser.Parse(input, out var delimiters);

            var numbers = _stringToNumberProcessor.Process(delimitedNumbers, delimiters);
            GuardAgainstNegativeNumbers(numbers);

            return Sum(numbers);
        }

        private static void GuardAgainstNegativeNumbers(int[] numbers)
        {
            var negativeNumbers = numbers.Where(n => n < 0).ToArray();

            if (negativeNumbers.Any())
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
        }

        private static int Sum(int[] numbers)
        {
            return numbers.Where(n => n > 0 && n < 1000).Sum();
        }
    }
}
