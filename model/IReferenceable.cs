using System;
using System.Collections.Generic;
namespace funda.model
{
	public interface IReferenceable
	{
		bool DisplayReferences { get; set; }
		List<Reference> References { get; set; }
	}
}