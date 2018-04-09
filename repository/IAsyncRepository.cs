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
		Task<AsyncResponse<List<T>>> ReadAllAsync();
        Task<AsyncResponse<T>> ReadAsync(int id);
        
		// UPDATE
        Task<AsyncResponse<T>> UpdateAsync(T obj);
        
		// DELETE
        Task<AsyncResponse<T>> DeleteAsync(T obj);

		void Initialize();
    }
}