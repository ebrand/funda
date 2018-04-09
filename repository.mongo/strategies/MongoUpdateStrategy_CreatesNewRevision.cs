using System;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;

namespace funda.repository.mongo
{
	public class MongoUpdateStrategy_CreatesNewRevision<T> : IUpdateStrategy<T> where T : IAuditable
	{
		public Task<AsyncResponse<T>> UpdateAsync(T obj, object collection)
		{
			throw new NotImplementedException();
		}
	}
}