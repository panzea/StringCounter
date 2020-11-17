using FluentAssertions;
using StringCounter.DelimiterParser;
using Xunit;

namespace StringCounter.Unit.Test.DelimiterParser
{
    public class SimpleDelimiterParserShould
    {
        [Fact]
        public void ReturnDefaultDelimiters()
        {
            // Arrange
            const string input = "5,6";
            var sut = new SimpleDelimiterParser();
            var expectedResult = new char[] {','};

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            result.Should().HaveSameCount(expectedResult);
            result.Should().Equal(expectedResult);
            remainingCharacters.Should().Be("5,6");
        }
    }
}
