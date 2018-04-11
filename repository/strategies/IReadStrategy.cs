using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.common.logging;
using funda.model;

namespace funda.repository.strategies
{
	public interface IReadStrategy<T>
	{
		Task<AsyncResponse<T>> ReadAsync(int id, object collection);
		Task<AsyncResponse<List<T>>> ReadAllAsync(object collection);
	}
}