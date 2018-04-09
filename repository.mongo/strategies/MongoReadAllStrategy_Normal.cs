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
	public class MongoReadAllStrategy_Normal<T> : IReadAllStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<List<T>>> ReadAllAsync(object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;
			
			sw.Start();
			var result = await Task.Run(() => mongoCollection);
			sw.Stop();

			// transform 'result' into a. DTO for trading lookup data information

			return new AsyncResponse<List<T>>(
				payload      : (List<T>)result,
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds
			);
		}
	}
}