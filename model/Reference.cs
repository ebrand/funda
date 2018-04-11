using System;
using funda.common;

namespace funda.model
{
	public class Reference : ISearchable
	{
		public string Name { get; set; }
		public string Url { get; set; }

		public Reference()
		{}
		public Reference(string name, string url)
		{
			this.Name = name;
			this.Url = url;
		}

		public bool ContainsSearchTerm(string searchTerm)
		{
			return
				   this.Name.Contains(searchTerm)
				|| this.Url.Contains(searchTerm);
		}
	}
}