using System;
using System.Collections.Generic;
using funda.common.auditing;

namespace funda.common
{
	public interface ISearchable
	{
		bool ContainsSearchTerm(string searchTerm);
	}
}