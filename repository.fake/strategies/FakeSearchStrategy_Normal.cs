using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.model;
using funda.repository.strategies;
using System.Diagnostics;
using funda.common;

namespace funda.repository.fake.strategies
{
	public class FakeSearchStrategy_Normal<T> : ISearchStrategy<T> where T : ISearchable
	{
		public async Task<AsyncResponse<List<T>>> KeywordSearchAsync(string searchTerm, object collection)
		{
			var sw = new Stopwatch();
			sw.Start();
			var fakeCollection = collection as List<T>;
			var result = await Task.Run(() => fakeCollection.FindAll(p => p.ContainsSearchTerm(searchTerm)));
			sw.Stop();

			return new AsyncResponse<List<T>>(
				payload      : result,
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