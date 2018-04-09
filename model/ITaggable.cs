using System;
using System.Collections.Generic;

namespace funda.model
{
	public interface ITaggable
	{
		List<string> Tags { get; set; }
	}
}