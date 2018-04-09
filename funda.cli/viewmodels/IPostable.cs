using System;
using funda.common.auditing;

namespace funda.cli.viewmodels
{
	public interface IPostable : IAuditable, ITaggable, ICommentable, ILinkable, IReferenceable
	{}
}