using System;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository
{
	public interface ICreateStrategy<T>
	{
		Task<AsyncResponse<T>> CreateAsync(T obj, object collection, IFundaLogger<T> logger);
	}
}
