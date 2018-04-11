using System;
using System.Collections.Generic;

namespace funda.repository
{
    public interface IAsyncResponse<T>
    {
		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		string Message { get; set; }

		/// <summary>
		/// Gets or sets the payload.
		/// </summary>
		/// <value>The payload.</value>
		List<T> Payload { get; set; }

		/// <summary>
		/// Gets or sets the type of the response.
		/// </summary>
		/// <value>The type of the response.</value>
		AsyncResponseType ResponseType { get; set; }

		/// <summary>
		/// Gets or sets the timing in ms.
		/// </summary>
		/// <value>The timing in ms.</value>
		long TimingInMs { get; set; }
    }
}