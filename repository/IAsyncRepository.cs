using System;
using System.Threading.Tasks;

namespace funda.repository
{
    public interface IAsyncRepository<T, TStrategyFactory>
    {
        // CREATE
        Task<AsyncResponse<T>> CreateAsync(T obj);
        // READ
        Task<AsyncResponse<T>> ReadAsync(int id);
        // UPDATE
        Task<AsyncResponse<T>> UpdateAsync(T obj);
        // DELETE
        Task<AsyncResponse<T>> DeleteAsync(T obj);
    }
}