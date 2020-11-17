using FluentAssertions;
using StringCounter.DelimiterParser;
using Xunit;

namespace StringCounter.Unit.Test.DelimiterParser
{
    public class MultiDelimiterParserShould
    {
        [Fact]
        public void ReturnDefaultDelimiters()
        {
            // Arrange
            const string input = "5,6";
            var sut = new MultiDelimiterParser();
            var expectedResult = new char[] {',', '\n'};

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            result.Should().HaveSameCount(expectedResult);
            result.Should().Equal(expectedResult);
            remainingCharacters.Should().Be("5,6");
        }

        [Fact]
        public void ReturnFullInputString_WhenUsingNewLineDelimiter()
        {
            // Arrange
            const string input = "5,6\n7,8\n9";
            var sut = new MultiDelimiterParser();

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            remainingCharacters.Should().Be("5,6\n7,8\n9");
        }
    }
}
