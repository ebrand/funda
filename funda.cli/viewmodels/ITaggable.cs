using System;
using System.Collections.Generic;

namespace funda.cli.viewmodels
{
	public interface ITaggable
	{
		List<string> Tags { get; set; }
	}
}