using System;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.common.logging;
using funda.repository.strategies;
using System.Collections.Generic;

namespace funda.repository
{
	public class AsyncRepository<T> : IAsyncRepository<T> where T : IAuditable
	{
		private readonly object _collection;
		private readonly IFundaLogger<T> _logger;
		public IStrategyFactory<T> StrategyFactory { get; private set; }

		public object Collection => _collection;

		public AsyncRepository(
			IFundaLogger<T> logger, 
			ICreateStrategy<T> createStrategy, 
			IReadStrategy<T> readStrategy, 
			IUpdateStrategy<T> updateStrategy, 
			IDeleteStrategy<T> deleteStrategy, 
			ISearchStrategy<T> searchStrategy
		){
			_logger = logger;
			this.StrategyFactory = 
				new StrategyFactory<T>(
					createStrategy, 
					readStrategy, 
					updateStrategy, 
					deleteStrategy, 
					searchStrategy
				);
		}

		public void Initialize()
		{ }

		public async Task<AsyncResponse<T>> CreateAsync(T obj)
		{
			try
			{
				return await this.StrategyFactory.Create.CreateAsync(obj, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Create.Failure,
					message: $"Failed to create object {obj.Identifier.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> ReadAllAsync()
		{
			try
			{
				return await this.StrategyFactory.Read.ReadAllAsync(Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Read.Failure,
					message: $"Failed to retrieve objects.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> ReadAsync(int id)
		{
			try
			{
				return await this.StrategyFactory.Read.ReadAsync(id, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Read.Failure,
					message: $"Failed to retrieve object {id.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> UpdateAsync(T obj)
		{
			try
			{
				return await this.StrategyFactory.Update.UpdateAsync(obj, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Update.Failure,
					message: $"Failed to update object {obj.Identifier.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> DeleteAsync(T obj)
		{
			try
			{
				return await this.StrategyFactory.Delete.DeleteAsync(obj, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Delete.Failure,
					message: $"Failed to mark object {obj.Identifier.ToString()} for deletion.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> KeywordSearchAsync(string searchTerm)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Search.InProgress, $"Performing keyword search using search term: '{searchTerm}'...");
				return await this.StrategyFactory.Search.KeywordSearchAsync(searchTerm, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Search.Failure,
					message: $"Failed to complete keyword search for search term: '{searchTerm}'.",
					exception: exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<T>> PropertySearch(List<SearchParameter> searchParameters)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Search.InProgress, $"Performing property search...");
				return await this.StrategyFactory.Search.PropertySearch(searchParameters, Collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Search.Failure,
					message: $"Failed to complete property search.",
					exception: exc
				);
				throw;
			}
		}
	}
}