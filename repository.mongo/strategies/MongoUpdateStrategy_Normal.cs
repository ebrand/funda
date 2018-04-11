using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.repository.strategies;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo
{
	public class MongoUpdateStrategy_Normal<T> : IUpdateStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> UpdateAsync(T obj, object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;
			var filter = new BsonDocument(
				new BsonElement("identifier", new BsonString($"{obj.Identifier}"))
			);

			Utilities.Auditing.AddUpdateAudit(obj);

			sw.Start();
			var result = await mongoCollection.ReplaceOneAsync(filter, obj.ToBsonDocument());
			sw.Stop();

			return new AsyncResponse<T>(
				payload      : new List<T>() { obj },
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds,
				message      : $"Ack: {result.IsAcknowledged.ToString()}, Modified count: {result.ModifiedCount.ToString()}"
			);
		}
	}
}