using System;
using System.Threading.Tasks;
using funda.repository.strategies;
using System.Collections.Generic;
using System.Linq;
using funda.common.auditing;

namespace funda.repository.fake.strategies
{
	public class FakeReadStrategy_Normal<T> : IReadStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> ReadAsync(int id, object collection)
		{
			var fakeCollection = collection as List<T>;
			var result = await Task.Run(() => fakeCollection.FirstOrDefault(p => p.Identifier == id));

			return new AsyncResponse<T>(
				payload      : result,
				responseType : AsyncResponseType.Success,
				timingInMs   : 0
			);
		}
	}
}