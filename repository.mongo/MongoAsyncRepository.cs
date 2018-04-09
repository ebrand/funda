using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.common.logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo
{
    public class AsyncRepository<T, TStrategyFactory> : IAsyncRepository<T, TStrategyFactory> where T : IIdentifiable, IAuditable
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly IFundaLogger<T> _logger;
		private readonly TStrategyFactory _strategyFactory;

		public AsyncRepository(IFundaLogger<T> logger, TStrategyFactory factory)
        {
            _logger = logger;
			_strategyFactory = factory;
        }

        public async Task<AsyncResponse<T>> CreateAsync(T obj)
        {
            try
            {
				Utilities.AddCreateAudit(obj);

                var sw = new Stopwatch();
                sw.Start();
                await _collection.InsertOneAsync(obj.ToBsonDocument());
                sw.Stop();
                var timing = sw.ElapsedMilliseconds;
                return new AsyncResponse<T>(
                    payload      : obj,
                    responseType : AsyncResponseType.Success,
                    timingInMs   : timing);
            }
            catch(Exception exc)
            {
                _logger.LogError(common.logging.Events.Repository.Create.Failure, exc.Message, exc);
                return new AsyncResponse<T>(
                    payload      : obj,
                    responseType : AsyncResponseType.Failure,
                    timingInMs   : 0);
            }
        }

        public async Task<AsyncResponse<T>> ReadAsync(int id)
        {
            try
            {
                var filter = new BsonDocument(
                    new BsonElement("identifier", new BsonString($"{id}"))
                );

                var sw = new Stopwatch();
                sw.Start();
                var result = await _collection.FindAsync<T>(filter);
                sw.Stop();
                var timing = sw.ElapsedMilliseconds;
                return new AsyncResponse<T>(
                    payload      : result.Single(),
                    responseType : AsyncResponseType.Success,
                    timingInMs   : timing
                );
            }
            catch(Exception exc)
            {
                _logger.LogError(common.logging.Events.Repository.Read.Failure, exc.Message, exc);
                return new AsyncResponse<T>(
                    payload      : default(T),
                    responseType : AsyncResponseType.Failure,
                    timingInMs   : 0,
                    message      : exc.Message);
            }
        }

        public async Task<AsyncResponse<T>> UpdateAsync(T obj)
        {
            try
            {
				return await _updateStrategy.UpdateAsync(obj, _collection, _logger);
            }
            catch(Exception exc)
            {
                _logger.LogError(common.logging.Events.Repository.Update.Failure, exc.Message, exc);
                return new AsyncResponse<T>(
                    payload      : obj,
                    responseType : AsyncResponseType.Failure,
                    timingInMs   : 0,
                    message      : exc.Message
                );
            }
        }

        public async Task<AsyncResponse<T>> DeleteAsync(T obj)
        {
            try
            {
				Utilities.AddDeleteAudit(obj);

				var filter = new BsonDocument(
                    new BsonElement("identifier", new BsonString($"{obj.Identifier}"))
                );

                var sw = new Stopwatch();
                sw.Start();
                var result = await _collection.DeleteOneAsync(filter);
                sw.Stop();
                var deletedCount = result.DeletedCount;
                var ack = result.IsAcknowledged;
                var timing = sw.ElapsedMilliseconds;
                return new AsyncResponse<T>(
                    payload      : obj,
                    responseType : AsyncResponseType.Success,
                    timingInMs   : timing,
                    message      : $"Ack: {ack.ToString()}, Delete count: {deletedCount.ToString()}"
                );
            }
            catch(Exception exc)
            {
                _logger.LogError(common.logging.Events.Repository.Delete.Failure, exc.Message, exc);
                return new AsyncResponse<T>(
                    payload      : obj,
                    responseType : AsyncResponseType.Failure,
                    timingInMs   : 0,
                    message      : exc.Message
                );
            }
        }
    }
}