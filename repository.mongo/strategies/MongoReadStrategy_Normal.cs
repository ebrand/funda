using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common.auditing;
using funda.repository.strategies;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo.strategies
{
	public class MongoReadStrategy_Normal<T> : IReadStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> ReadAsync(int id, object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;
			var filter = new BsonDocument(
				new List<BsonElement>
				{
					new BsonElement("identifier", new BsonString($"{id}")),
					new BsonElement("deleteflag", new BsonBoolean(false))
				}
			);

			sw.Start();
			var result = await mongoCollection.FindAsync<T>(filter);
			sw.Stop();

			return new AsyncResponse<T>(
				payload      : (List<T>)result,
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds
			);
		}

		public async Task<AsyncResponse<T>> ReadAllAsync(object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;

			sw.Start();
			var result = await Task.Run(() => mongoCollection);
			sw.Stop();

			// transform 'result' into a. DTO for trading lookup data information

			return new AsyncResponse<T>(
				payload: (List<T>)result,
				responseType: AsyncResponseType.Success,
				timingInMs: sw.ElapsedMilliseconds
			);
		}
	}
}