using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using funda.model;

namespace funda.repository.strategies
{
	public interface ISearchStrategy<T>
	{
		/// <summary>
		/// Performs a full-text search of documents/records.
		/// </summary>
		/// <returns>An <see cref="AsyncResponse" /> containing the search status and results (Payload).</returns>
		/// <param name="searchTerm">Search term.</param>
		/// <param name="collection">The collection to search.</param>
		Task<AsyncResponse<T>> KeywordSearchAsync(string searchTerm, object collection);

		/// <summary>
		/// Performs a search for the properties and search terms provided.
		/// </summary>
		/// <returns>An <see cref=" cref="AsyncResponse/> containing the search status and results (Payload).</returns>
		/// <param name="searchParameters">A List<<see cref="SearchParameter"/>> representing the property names and values to search.</param>
		/// <param name="collection">The collection to search.</param>
		Task<AsyncResponse<T>> PropertySearch(List<SearchParameter> searchParameters, object collection);
	}
}