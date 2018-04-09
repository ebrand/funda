using System;
using System.Collections.Generic;

namespace funda.cli.viewmodels
{
	public interface IReferenceable
	{
		bool DisplayReferences { get; set; }
		List<Reference> References { get; set; }
	}
}