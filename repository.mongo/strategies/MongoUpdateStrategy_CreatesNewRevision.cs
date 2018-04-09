using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using funda.common.logging;

namespace funda.repository.mongo
{
	public class MongoUpdateStrategy_CreatesNewRevision<T> : IUpdateStrategy<T>
	{
		public Task<AsyncResponse<T>> UpdateAsync(T obj, object collection, IFundaLogger<T> logger)
		{
			throw new NotImplementedException();
		}
	}
}