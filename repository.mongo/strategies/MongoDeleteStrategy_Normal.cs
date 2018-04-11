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
	public class MongoDeleteStrategy_Normal<T> : IDeleteStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> DeleteAsync(T obj, object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;
			var filter = new BsonDocument(
				new BsonElement("identifier", new BsonString($"{obj.Identifier.ToString()}"))
			);

			sw.Start();
			var result = await mongoCollection.DeleteOneAsync(filter);
			sw.Stop();

			return new AsyncResponse<T>(
				payload      : new List<T>() { obj },
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds,
				message      : $"Ack: {result.IsAcknowledged.ToString()}. Object {obj.Identifier.ToString()} permanently deleted."
			);
		}
	}
}