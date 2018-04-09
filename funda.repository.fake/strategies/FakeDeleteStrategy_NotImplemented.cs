using System;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;

namespace funda.repository.fake.strategies
{
	public class FakeDeleteStrategy_NotImplemented<T> : IDeleteStrategy<T> where T : IAuditable
	{
		public Task<AsyncResponse<T>> DeleteAsync(T obj, object collection)
		{
			throw new NotImplementedException();
		}
	}
}