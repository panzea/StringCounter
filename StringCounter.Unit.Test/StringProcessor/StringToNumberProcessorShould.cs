using System;
using FluentAssertions;
using StringCounter.StringProcessor;
using Xunit;

namespace StringCounter.Unit.Test.StringProcessor
{
    public class StringToNumberProcessorShould
    {
        [Fact]
        public void ReturnEmptyArray_WhenInputContainsNoItems()
        {
            // Arrange
            const string input = "";
            var delimiters = new [] {','};
            var sut = new StringToNumberProcessor();
            var expectedResult = Array.Empty<int>();

            // Act
            var result = sut.Process(input, delimiters);

            // Assert
            result.Should().Equal(expectedResult);
        }

        [Fact]
        public void ReturnSingleItem_WhenInputContainsOneItem()
        {
            // Arrange
            const string input = "7";
            var delimiters = new [] { ',' };
            var sut = new StringToNumberProcessor();
            var expectedResult = new [] { 7 };

            // Act
            var result = sut.Process(input, delimiters);

            // Assert
            result.Should().Equal(expectedResult);
        }

        [Fact]
        public void ReturnMultipleItems_WhenInputContainsMultipleItems()
        {
            // Arrange
            const string input = "5,6";
            var delimiters = new [] { ',' };
            var sut = new StringToNumberProcessor();
            var expectedResult = new [] { 5, 6 };

            // Act
            var result = sut.Process(input, delimiters);

            // Assert
            result.Should().Equal(expectedResult);
        }


        [Fact]
        public void ThrowException_WhenInputContainsConsecutiveDelimiters()
        {
            // Arrange
            const string input = "1,,";
            var delimiters = new [] { ',' };
            var sut = new StringToNumberProcessor();

            // Act
            void Act() => sut.Process(input, delimiters);

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Delimiters must not be used consecutively");
        }

        [Fact]
        public void ThrowException_WhenInputContainsInvalidCharacterFormat()
        {
            // Arrange
            const string input = "1,a";
            var delimiters = new[] { ',' };
            var sut = new StringToNumberProcessor();

            // Act
            void Act() => sut.Process(input, delimiters);

            // Assert
            var exception = Assert.Throws<FormatException>(Act);
        }

        [Fact]
        public void ReturnMultipleItems_WhenInputUsesDelimiterWithDifferentCaseToDefinition()
        {
            // Arrange
            const string input = "1A2a3";
            var delimiters = new [] { 'a' };
            var sut = new StringToNumberProcessor();
            var expectedResult = new [] {1, 2, 3};

            // Act
            var result = sut.Process(input, delimiters);

            // Assert
            result.Should().Equal(expectedResult);
        }
    }
}
