using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.common.logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo
{
	public class MongoUpdateStrategy_Normal<T> : IUpdateStrategy<T> where T : IIdentifiable, IAuditable
	{
		public async Task<AsyncResponse<T>> UpdateAsync(T obj, object collection, IFundaLogger<T> logger)
		{
			try
			{
				Utilities.AddUpdateAudit(obj);

				var filter = new BsonDocument(
					new BsonElement("identifier", new BsonString($"{obj.Identifier}"))
				);

				var sw = new Stopwatch();
				sw.Start();
				var result = await (collection as IMongoCollection<BsonDocument>).ReplaceOneAsync(filter, obj.ToBsonDocument());
				sw.Stop();
				var ack = result.IsAcknowledged;
				var modifiedCount = result.ModifiedCount;
				var timing = sw.ElapsedMilliseconds;
				return new AsyncResponse<T>(
					payload      : obj,
					responseType : AsyncResponseType.Success,
					timingInMs   : timing,
					message      : $"Ack: {ack.ToString()}, Modified count: {modifiedCount.ToString()}"
				);
			}
			catch(Exception exc)
			{
				logger.LogError(common.logging.Events.Repository.Update.Failure, exc.Message, exc);
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