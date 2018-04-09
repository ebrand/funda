using System;

namespace funda.repository
{
    public interface IAsyncResponse<T>
    {
        string Message { get; set; }
        T Payload { get; set; }
        AsyncResponseType ResponseType { get; set; }
        long TimingInMs { get; set; }
    }
}