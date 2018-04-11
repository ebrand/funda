using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;
using System.Linq;

namespace funda.repository.fake.strategies
{
	public class FakeCreateStrategy_Normal<T> : ICreateStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> CreateAsync(T obj, object collection)
		{
			var fakeCollection = collection as List<T>;
			var existing = fakeCollection.FirstOrDefault(p => p.Identifier == obj.Identifier);
			if(existing == null)
			{
				await Task.Run(() => fakeCollection.Add(obj));
				return new AsyncResponse<T>(
					payload      : new List<T>() { obj },
					responseType : AsyncResponseType.Success,
					timingInMs   : 0
				);
			}
			else
			{
				return new AsyncResponse<T>(
					payload      : new List<T>() { obj },
					responseType : AsyncResponseType.Failure,
					timingInMs   : 0,
					message      : "The object provided already exists."
				);
			}
		}
	}
}