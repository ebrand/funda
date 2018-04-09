using System;
using System.Collections.Generic;

namespace funda.model
{
	public interface ICommentable
	{
		bool AllowComments { get; set; }
		List<Comment> Comments { get; set; }
	}
}