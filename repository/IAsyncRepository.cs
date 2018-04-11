using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.repository.strategies;

namespace funda.repository
{
    public interface IAsyncRepository<T>
    {
		IStrategyFactory<T> StrategyFactory { get; }
        
		// CREATE
        Task<AsyncResponse<T>> CreateAsync(T obj);
		
		// READ
        Task<AsyncResponse<T>> ReadAsync(int id);
		Task<AsyncResponse<List<T>>> ReadAllAsync();

		// UPDATE
        Task<AsyncResponse<T>> UpdateAsync(T obj);
        
		// DELETE
        Task<AsyncResponse<T>> DeleteAsync(T obj);

		// SEARCH
		Task<AsyncResponse<List<T>>> KeywordSearchAsync(string searchTerm, object collection);
		Task<AsyncResponse<List<T>>> PropertySearch(List<SearchParameter> searchParameters, object collection);

		void Initialize();
    }
}