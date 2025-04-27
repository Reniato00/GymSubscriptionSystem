using System.Text.RegularExpressions;
using Bussines.Decorators;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Bussines.Decorators
{
    public class LoggerDecoratorTests
    {
        private ILoggerDecorator<LoggerDecoratorTests> loggerDecorator = null!;
        private readonly Mock<ILogger<LoggerDecoratorTests>> loggerMock = new();

        public LoggerDecoratorTests()
        {
            this.loggerDecorator = new LoggerDecorator<LoggerDecoratorTests>(loggerMock.Object);
        }

        [Fact]
        public void LogInformation_ShouldLogCorrectly()
        {
            // Arrange
            var message = "Test message";
            string expectedMessage = $"The instructor {message} test the customer args";
            // Act
            loggerDecorator.LogInformation(message, "args", "test");

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(expectedMessage)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            loggerMock.VerifyLog(LogLevel.Information, Times.Once(), expectedMessage);
        }
    }

    static class MockHelper
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> logger, LogLevel level, Times times, string? regex = null) =>
            logger.Verify(m => m.Log(
            level,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((x, y) => regex == null || Regex.IsMatch(x.ToString(), regex)),
            It.IsAny<Exception?>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}