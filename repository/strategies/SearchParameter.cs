using System;
namespace funda.repository.strategies
{
	public class SearchParameter
	{
		public string PropertyName { get; set; }
		public string SearchTerm { get; set; }

		public SearchParameter(){}
		public SearchParameter(string propertyName, string searchTerm)
		{
			this.PropertyName = propertyName;
			this.SearchTerm = searchTerm;
		}
	}
}