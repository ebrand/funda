using System;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;

namespace funda.repository.fake.strategies
{
	public class FakeUpdateStrategy_NotImplemented<T> : IUpdateStrategy<T> where T : IAuditable
	{
		public Task<AsyncResponse<T>> UpdateAsync(T obj, object collection)
		{
			throw new NotImplementedException();
		}
	}
}