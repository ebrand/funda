using System;

namespace funda.common.logging
{
    public interface IFundaLogger<T>
    {
        void LogDebug(int eventId, string message, Exception exception = null);
        void LogError(int eventId, string message, Exception exception = null);
        void LogInfo(int eventId, string message, Exception exception = null);
        void LogTrace(int eventId, string message, Exception exception = null);
    }
}