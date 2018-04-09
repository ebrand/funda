using System;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository.strategies
{
	public interface IReadStrategy<T>
	{
		Task<AsyncResponse<T>> ReadAsync(int id, object collection);
	}
}