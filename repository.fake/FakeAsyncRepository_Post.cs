using funda.common.auditing;
using funda.common.logging;
using funda.model;
using funda.model.fakes;
using funda.repository.strategies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace funda.repository.fake
{
	public class FakeAsyncRepository_Post : IAsyncRepository<Post>
	{
		private List<Post> _collection;
		private readonly IFundaLogger<Post> _logger;
		public IStrategyFactory<Post> StrategyFactory { get; private set; }

		public FakeAsyncRepository_Post(IStrategyFactory<Post> strategyFactory, IFundaLogger<Post> logger)
		{
			_logger = logger;
			this.StrategyFactory = strategyFactory;

			_logger = logger;
			if (_collection == null)
				_collection = new List<Post>();

			// if this fake repository is constructed via the DI container, it will
			// create an initial 1,000 fake posts by deserializing them from a
			// local file ("fakedata/1000posts.min.json"). Subsequently calling the
			// "Initialize()" method manually will replace these default
			// customers with the provided arbitrary number of customers created
			// using Faker (Bogus).
			using (StreamReader file = File.OpenText(@"fakedata/1000posts.min.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				var sw = new Stopwatch();
				sw.Start();
				_collection = ((Post[])serializer.Deserialize(file, typeof(Post[]))).ToList();
				sw.Stop();
				var elapsedMs = sw.ElapsedMilliseconds;
				_logger.LogInfo(0, $"Loaded fake posts from file system in {elapsedMs.ToString()} ms.");
			}
		}
		public void Initialize()
		{
			// create a bunch of fake posts

			var sw = new Stopwatch();
			sw.Start();
			_collection = FakeFactory.PostFaker.Generate(1000);
			sw.Stop();

			Console.WriteLine($"Fake posts initialized using Faker (Bogus) in {sw.ElapsedMilliseconds.ToString()} ms.");
		}

		// CREATE
		public async Task<AsyncResponse<Post>> CreateAsync(Post obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Create new object...");
				return await this.StrategyFactory.Create.CreateAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Create.Failure,
					message   : $"Failed to retrieve objects.",
					exception : exc
				);
				throw;
			}
		}

		// READ
		public async Task<AsyncResponse<Post>> ReadAllAsync()
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Retrieving all objects...");
				return await this.StrategyFactory.Read.ReadAllAsync(_collection);
			}
			catch (Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Read.Failure,
					message   : $"Failed to retrieve all objects.",
					exception : exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<Post>> ReadAsync(int id)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Retrieving object ID:{id.ToString()}...");
				return await this.StrategyFactory.Read.ReadAsync(id, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Read.Failure,
					message   : $"Failed to retrieve object ID:{id.ToString()}.",
					exception : exc
				);
				throw;
			}
		}

		// UPDATE
		public async Task<AsyncResponse<Post>> UpdateAsync(Post obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Updating object ID:{obj.Identifier.ToString()}...");
				return await this.StrategyFactory.Update.UpdateAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Update.Failure,
					message   : $"Failed to update object ID:{obj.Identifier.ToString()}.",
					exception : exc
				);
				throw;
			}
		}

		// DELETE
		public async Task<AsyncResponse<Post>> DeleteAsync(Post obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Deleting object ID:{obj.Identifier.ToString()}...");
				return await this.StrategyFactory.Delete.DeleteAsync(obj, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Delete.Failure,
					message   : $"Failed to delete object ID:{obj.Identifier.ToString()}.",
					exception : exc
				);
				throw;
			}
		}

		// SEARCH
		public async Task<AsyncResponse<Post>> KeywordSearchAsync(string searchTerm)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Search.InProgress, $"Performing keyword search using search term: '{searchTerm}'...");
				return await this.StrategyFactory.Search.KeywordSearchAsync(searchTerm, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Search.Failure,
					message   : $"Failed to complete keyword search for search term: '{searchTerm}'.",
					exception : exc
				);
				throw;
			}
		}

		public async Task<AsyncResponse<Post>> PropertySearch(List<SearchParameter> searchParameters)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Search.InProgress, $"Performing property search...");
				return await this.StrategyFactory.Search.PropertySearch(searchParameters, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId   : Events.Repository.Search.Failure,
					message   : $"Failed to complete property search.",
					exception : exc
				);
				throw;
			}
		}
	}
}