using System;

namespace funda.cli.viewmodels
{
	public class Reference
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
	}
}