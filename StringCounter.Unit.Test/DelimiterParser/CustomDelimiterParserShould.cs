using FluentAssertions;
using StringCounter.DelimiterParser;
using Xunit;

namespace StringCounter.Unit.Test.DelimiterParser
{
    public class CustomDelimiterParserShould
    {
        [Fact]
        public void ReturnDefaultDelimiters_WhenNoDelimiterPatternSpecified()
        {
            // Arrange
            const string input = "5,6";
            var sut = new CustomDelimiterParser();
            var expectedResult = new char[] {',', '\n'};

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            result.Should().HaveSameCount(expectedResult);
            result.Should().Equal(expectedResult);
            remainingCharacters.Should().Be("5,6");
        }

        [Fact]
        public void ReturnDefaultDelimiters_WhenNoDelimitersSpecifiedInDelimiterPattern()
        {
            // Arrange
            const string input = "//\n5,6";
            var sut = new CustomDelimiterParser();
            var expectedResult = new char[] { ',', '\n' };

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            result.Should().HaveSameCount(expectedResult);
            result.Should().Equal(expectedResult);
            remainingCharacters.Should().Be("5,6");
        }

        [Fact]
        public void ReturnValidDelimiters_WhenSingleDelimiterProvided()
        {
            // Arrange
            const string input = "//*\n5,6";
            var sut = new CustomDelimiterParser();
            var expectedResult = new char[] { '*', ',', '\n' };

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            result.Should().HaveSameCount(expectedResult);
            result.Should().Equal(expectedResult);
            remainingCharacters.Should().Be("5,6");
        }

        [Fact]
        public void ReturnValidDelimiters_WhenMultipleDelimiterProvided()
        {
            // Arrange
            const string input = "//*%;\n5,6";
            var sut = new CustomDelimiterParser();
            var expectedResult = new char[] { '*', '%', ';', ',', '\n' };

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
            var sut = new CustomDelimiterParser();

            // Act
            var remainingCharacters = sut.Parse(input, out var result);

            // Assert
            remainingCharacters.Should().Be("5,6\n7,8\n9");
        }
    }
}
