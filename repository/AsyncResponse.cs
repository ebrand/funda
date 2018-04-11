using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace funda.repository
{
    public class AsyncResponse<T> : IAsyncResponse<T>
    {
        public string Message { get; set; }
        
        public List<T> Payload { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AsyncResponseType ResponseType { get; set; }
        
        public long TimingInMs { get; set; }

        public AsyncResponse(List<T> payload, AsyncResponseType responseType, long timingInMs, string message = "")
        {
            this.Message = message;
            this.Payload = payload;
            this.ResponseType = responseType;
            this.TimingInMs = timingInMs;
        }
    }
}