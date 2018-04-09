using System;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;

namespace funda.repository.fake.strategies
{
	public class FakeCreateStrategy_NotImplemented<T> : ICreateStrategy<T> where T : IAuditable
	{
		public Task<AsyncResponse<T>> CreateAsync(T obj, object collection)
		{
			throw new NotImplementedException();
		}
	}
}