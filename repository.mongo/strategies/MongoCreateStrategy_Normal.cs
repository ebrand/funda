﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using funda.common;
using funda.common.auditing;
using funda.repository.strategies;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace funda.repository.mongo.strategies
{
	public class MongoCreateStrategy_Normal<T> : ICreateStrategy<T> where T : IAuditable
	{
		public async Task<AsyncResponse<T>> CreateAsync(T obj, object collection)
		{
			var sw = new Stopwatch();
			var mongoCollection = collection as IMongoCollection<BsonDocument>;

			Utilities.Auditing.AddCreateAudit(obj);

			sw.Start();
			await mongoCollection.InsertOneAsync(obj.ToBsonDocument());
			sw.Stop();

			return new AsyncResponse<T>(
				payload      : new List<T> { obj },
				responseType : AsyncResponseType.Success,
				timingInMs   : sw.ElapsedMilliseconds
			);
		}
	}
}