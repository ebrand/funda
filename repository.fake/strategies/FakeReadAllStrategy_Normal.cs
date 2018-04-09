using System;
using System.Threading.Tasks;
using funda.repository.strategies;
using System.Collections.Generic;
using funda.common.auditing;

namespace funda.repository.fake.strategies
{
	public class FakeReadAllStrategy_Normal<T> : IReadAllStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<List<T>>> ReadAllAsync(object collection)
		{
			var fakeCollection = collection as List<T>;
			var result = await Task.Run(() => fakeCollection);

			return new AsyncResponse<List<T>>(
				payload      : result,
				responseType : AsyncResponseType.Success,
				timingInMs   : 0
			);
		}
	}
}