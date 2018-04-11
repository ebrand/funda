using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.repository.strategies;
using MongoDB.Bson;
using MongoDB.Driver;

namespace funda.repository.mongo.strategies
{
	public class MongoSearchStrategy_Normal<T> : ISearchStrategy<T> where T : ISearchable
	{
		public async Task<AsyncResponse<List<T>>> KeywordSearchAsync(string searchTerm, object collection)
		{
			var sw = new Stopwatch();
			sw.Start();

			var mongoCollection = collection as IMongoCollection<BsonDocument>;

			// create a full-text filter on all document properties
			mongoCollection.Indexes.CreateOne(Builders<BsonDocument>.IndexKeys.Text("$**"));

			// define a seach filter that does a text search on all document properties ($**)
			var filter = Builders<BsonDocument>.Filter.Text(searchTerm);

			// search the Mongo collection using the filter above
			var result = await mongoCollection.FindAsync<T>(filter);

			sw.Stop();

			return new AsyncResponse<List<T>>(
				payload      : result.ToList(),
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds
			);
		}

		public Task<AsyncResponse<List<T>>> PropertySearch(List<SearchParameter> searchParameters, object collection)
		{
			throw new NotImplementedException();
		}
	}
}