using System;
using Microsoft.Extensions.Logging;

namespace funda.common.logging
{
    public class FundaMicrosoftLogger<T> : IFundaLogger<T>
    {
        private readonly ILogger<T> _logger;

		public FundaMicrosoftLogger()
		{
			_logger = ApplicationLogging.CreateLogger<T>();
		}

		public void LogDebug(int eventId, string message, Exception exception = null)
        {
            _logger.LogDebug(new EventId(eventId), exception, message);
        }
        public void LogError(int eventId, string message, Exception exception = null)
        {
            _logger.LogError(new EventId(eventId), exception, message);
        }
        public void LogInfo(int eventId, string message, Exception exception = null)
        {
            _logger.LogInformation(new EventId(eventId), exception, message);
        }
        public void LogTrace(int eventId, string message, Exception exception = null)
        {
            _logger.LogTrace(new EventId(eventId), exception, message);
        }
    }
}