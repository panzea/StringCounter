using System;
using FluentAssertions;
using Moq;
using StringCounter.DelimiterParser;
using StringCounter.StringProcessor;
using Xunit;

namespace StringCounter.Unit.Test
{
    public class StringCounterShould
    {
        [Fact]
        public void ReturnZero_WhenEmptyString()
        {
            // Arrange
            const string input = "";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] { ',' }))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(Array.Empty<int>());
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            var result = sut.Add(input);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void ReturnSummedValues_WhenInputIsValidSingleNumber()
        {
            // Arrange
            const string input = "7";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] { ',' }))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(new[] {7});
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            var result = sut.Add(input);

            // Assert
            result.Should().Be(7);
        }

        [Fact]
        public void ReturnSummedValues_WhenInputIsMultipleValidNumbers()
        {
            // Arrange
            const string input = "7,10";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] { ',' }))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(new[] {7, 10});
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            var result = sut.Add(input);

            // Assert
            result.Should().Be(17);
        }

        [Fact]
        public void ThrowException_WhenInputContainsSingleNegativeNumber()
        {
            // Arrange
            const string input = "1,-100";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] { ',' }))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(new[] {1, -100});
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            void Act() => sut.Add(input);

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Negatives not allowed: -100");
        }

        [Fact]
        public void ThrowException_WhenInputContainsMultipleNegativeNumbers()
        {
            // Arrange
            const string input = "-5,1,-100";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] { ',' }))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(new[] {-5, 1, -100});
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            void Act() => sut.Add(input);

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Negatives not allowed: -5, -100");
        }

        [Fact]
        public void IgnoreNumbersOver1000()
        {
            // Arrange
            const string input = "2,1001,13";

            char[] delimiters;
            var mockDelimiterProcessor = new Mock<IDelimiterParser>();
            mockDelimiterProcessor.Setup(m => m.Parse(It.IsAny<string>(), out delimiters))
                .Callback(new DelimiterParserCallbackDelegate((string input, out char[] delims) =>
                    delims = new char[] {','}))
                .Returns(input);

            var stringProcessor = AssumeStringToNumberProcessor(new[] {2, 1001, 13});
            var sut = AssumeStringCounter(mockDelimiterProcessor.Object, stringProcessor);

            // Act
            var result = sut.Add(input);

            // Assert
            result.Should().Be(15);
        }

        private StringCounter AssumeStringCounter(IDelimiterParser delimiterParser,
            IStringProcessor<int> stringProcessor)
        {
            return new StringCounter(delimiterParser, stringProcessor);
        }

        private IStringProcessor<int> AssumeStringToNumberProcessor(int[] processOutput)
        {
            var mockStringProcessor = new Mock<IStringProcessor<int>>();
            mockStringProcessor.Setup(m => m.Process(It.IsAny<string>(), It.IsAny<char[]>())).Returns(processOutput);

            return mockStringProcessor.Object;
        }

        delegate void DelimiterParserCallbackDelegate(string input, out char[] delims);
    }
}
