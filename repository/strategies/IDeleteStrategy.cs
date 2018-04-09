using System;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository.strategies
{
	public interface IDeleteStrategy<T>
	{
		Task<AsyncResponse<T>> DeleteAsync(T obj, object collection);
	}
}