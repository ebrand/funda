using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.repository.strategies;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo.strategies
{
	public class MongoDeleteStrategy_MarkForDeletion<T> : IDeleteStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> DeleteAsync(T obj, object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;
			var filter = new BsonDocument(
				new BsonElement("identifier", new BsonString($"{obj.Identifier.ToString()}"))
			);

			obj.DeleteFlag = true;
			Utilities.Auditing.AddDeleteAudit(obj);

			sw.Start();
			var result = await mongoCollection.UpdateOneAsync(filter, obj.ToBsonDocument());
			sw.Stop();

			return new AsyncResponse<T>(
				payload      : obj,
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds,
				message      : $"Ack: {result.IsAcknowledged.ToString()}. Object {obj.Identifier.ToString()} marked for deletion."
			);
		}
	}
}