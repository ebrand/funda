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
				payload      : result.Single(),
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds
			);
		}
	}
}