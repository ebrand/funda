using System;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository.strategies
{
	public interface IUpdateStrategy<T>
	{
		Task<AsyncResponse<T>> UpdateAsync(T obj, object collection);
	}
}