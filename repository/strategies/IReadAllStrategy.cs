using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace funda.repository.strategies
{
	public interface IReadAllStrategy<T>
	{
		Task<AsyncResponse<List<T>>> ReadAllAsync(object collection);
	}
}
