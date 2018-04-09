using System;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository.strategies
{
	public interface ICreateStrategy<T>
	{
		Task<AsyncResponse<T>> CreateAsync(T obj, object collection);
	}
}
