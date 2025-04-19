using Microsoft.Extensions.Logging;

namespace Bussines.Decorators
{
    public interface ILoggerDecorator<T>
    {
        void LogInformation(string instructor, string customer , string action);
    }
    public class LoggerDecorator<T> :ILoggerDecorator<T>
    {
        private readonly ILogger<T> logger;
        public LoggerDecorator(ILogger<T> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogInformation(string instructor, string customer , string action)
        {
            string message = $"The instructor {instructor} {action} the customer {customer}";
            logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            logger.LogError(message);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }
    }
}