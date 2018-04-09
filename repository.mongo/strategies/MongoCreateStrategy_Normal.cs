using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.common.logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace funda.repository.mongo
{
	public class MongoCreateStrategy_Normal<T> : ICreateStrategy<T> where T : IIdentifiable, IAuditable
	{
		public async Task<AsyncResponse<T>> CreateAsync(T obj, object collection, IFundaLogger<T> logger)
		{
			try
			{
				var mongoCollection = collection as IMongoCollection<BsonDocument>;

				Utilities.AddCreateAudit(obj);

				var sw = new Stopwatch();
				sw.Start();
				await mongoCollection.InsertOneAsync(obj.ToBsonDocument());
				sw.Stop();
				var timing = sw.ElapsedMilliseconds;
				return new AsyncResponse<T>(
					payload      : obj,
					responseType : AsyncResponseType.Success,
					timingInMs   : timing);
			}
			catch(Exception exc)
			{
				logger.LogError(common.logging.Events.Repository.Create.Failure, exc.Message, exc);
				return new AsyncResponse<T>(
					payload      : obj,
					responseType : AsyncResponseType.Failure,
					timingInMs   : 0);
			}
		}
	}
}