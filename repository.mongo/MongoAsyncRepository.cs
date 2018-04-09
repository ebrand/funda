using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.common.logging;
using funda.repository.strategies;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.Serialization;

namespace funda.repository.mongo
{
	public class MongoAsyncRepository<T> : IAsyncRepository<T> where T : IAuditable
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly IFundaLogger<T> _logger;
		public IStrategyFactory<T> StrategyFactory { get; private set; }

		public MongoAsyncRepository(IFundaLogger<T> logger, IStrategyFactory<T> factory)
        {
            _logger = logger;
			this.StrategyFactory = factory;
        }

		public void Initialize()
		{
			
		}

        public async Task<AsyncResponse<T>> CreateAsync(T obj)
        {
			try
			{
				return await this.StrategyFactory.Create.CreateAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Create.Failure,
					message   : $"Failed to create object {obj.Identifier.ToString()}.",
					exception : exc
				);
				throw;
			}
        }

        public async Task<AsyncResponse<T>> ReadAsync(int id)
        {
			try
			{
				return await this.StrategyFactory.Read.ReadAsync(id, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Read.Failure,
					message   : $"Failed to retrieve object {id.ToString()}.",
					exception : exc
				);
				throw;
			}
        }

        public async Task<AsyncResponse<T>> UpdateAsync(T obj)
        {
			try
			{
				return await this.StrategyFactory.Update.UpdateAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Update.Failure,
					message   : $"Failed to update object {obj.Identifier.ToString()}.",
					exception : exc
				);
				throw;
			}
        }

        public async Task<AsyncResponse<T>> DeleteAsync(T obj)
        {
			try
			{
				return await this.StrategyFactory.Delete.DeleteAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Delete.Failure,
					message   : $"Failed to mark object {obj.Identifier.ToString()} for deletion.",
					exception : exc
				);
				throw;
			}
        }
    }
}