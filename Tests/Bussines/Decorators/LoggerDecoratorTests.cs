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

            // Act
            loggerDecorator.LogInformation(message, "args", "test");

            // Assert
            // Verify that the log was written correctly (this would depend on your logging framework)
            // For example, if using Serilog, you could check the log file or output
        }
    }
}