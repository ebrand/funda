using System;
using funda.common;

namespace funda.repository.strategies
{
	public interface IStrategyFactory<T>
	{
		ICreateStrategy<T>  Create  { get; set; }
		IReadStrategy<T>    Read    { get; set; }
		IUpdateStrategy<T>  Update  { get; set; }
		IDeleteStrategy<T>  Delete  { get; set; }

		ISearchStrategy<T> Search { get; set; }
	}
}