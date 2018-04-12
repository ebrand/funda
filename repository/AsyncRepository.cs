﻿using funda.common;
using funda.common.auditing;
using funda.common.logging;
using funda.repository.strategies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace funda.repository
{
	public class AsyncRepository<T> : IAsyncRepository<T> where T : IAuditable
	{
		private readonly object _collection;
		private readonly IFundaLogger<T> _logger;
		public IStrategyFactory<T> StrategyFactory { get; private set; }

		public AsyncRepository(IStrategyFactory<T> strategyFactory, IFundaLogger<T> logger)
		{
			_logger = logger;
			this.StrategyFactory = strategyFactory;

			_logger = logger;
			if (_collection == null)
				_collection = new List<T>();

			if (Utilities.Configuration.UseFakeData)
			{
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
					_collection = ((T[])serializer.Deserialize(file, typeof(T[]))).ToList();
					sw.Stop();
					var elapsedMs = sw.ElapsedMilliseconds;
					_logger.LogInfo(0, $"Loaded fake posts from file system in {elapsedMs.ToString()} ms.");
				}
			}
		}

		public void Initialize()
		{ }

		// CREATE
		public async Task<AsyncResponse<T>> CreateAsync(T obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Create.InProgress, $"Creating new object...");
				return await this.StrategyFactory.Create.CreateAsync(obj, _collection);
			}
			catch (Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Create.Failure,
					message: $"Failed to create object {obj.Identifier.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		// READ
		public async Task<AsyncResponse<T>> ReadAllAsync()
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Reading all objects...");
				return await this.StrategyFactory.Read.ReadAllAsync(_collection);
			}
			catch (Exception exc)
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
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Reading object ID:{id.ToString()}...");
				return await this.StrategyFactory.Read.ReadAsync(id, _collection);
			}
			catch (Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Read.Failure,
					message: $"Failed to retrieve object ID:{id.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		// UPDATE
		public async Task<AsyncResponse<T>> UpdateAsync(T obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Update.InProgress, $"Updating object ID:{obj.Identifier.ToString()}...");
				return await this.StrategyFactory.Update.UpdateAsync(obj, _collection);
			}
			catch (Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Update.Failure,
					message: $"Failed to update object ID:{obj.Identifier.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		// DELETE
		public async Task<AsyncResponse<T>> DeleteAsync(T obj)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Delete.InProgress, $"Deleting object ID:{obj.Identifier.ToString()}...");
				return await this.StrategyFactory.Delete.DeleteAsync(obj, _collection);
			}
			catch (Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Delete.Failure,
					message: $"Failed to delete object ID:{obj.Identifier.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		// SEARCH
		public async Task<AsyncResponse<T>> KeywordSearchAsync(string searchTerm)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Search.InProgress, $"Performing keyword search using search term: '{searchTerm}'...");
				return await this.StrategyFactory.Search.KeywordSearchAsync(searchTerm, _collection);
			}
			catch (Exception exc)
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
				return await this.StrategyFactory.Search.PropertySearch(searchParameters, _collection);
			}
			catch (Exception exc)
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