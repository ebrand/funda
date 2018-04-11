using System;
using System.Threading.Tasks;
using funda.repository.strategies;
using System.Collections.Generic;
using System.Linq;
using funda.model;

namespace funda.repository.fake.strategies
{
	public class FakeReadStrategy_Normal<T> : IReadStrategy<T> where T : IPostable
	{
		public async Task<AsyncResponse<T>> ReadAsync(int id, object collection)
		{
			var fakeCollection = collection as List<T>;
			var result = await Task.Run(() => fakeCollection.FirstOrDefault(p => p.Identifier == id));

			return new AsyncResponse<T>(
				payload      : new List<T>() { result },
				responseType : AsyncResponseType.Success,
				timingInMs   : 0
			);
		}

		public async Task<AsyncResponse<T>> ReadAllAsync(object collection)
		{
			var fakeCollection = collection as List<T>;
			var result = await Task.Run(() => fakeCollection);

			return new AsyncResponse<T>(
				payload      : result,
				responseType : AsyncResponseType.Success,
				timingInMs   : 0
			);
		}
	}
}