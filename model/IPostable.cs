﻿using System;
using funda.common;
using funda.common.auditing;

namespace funda.model
{
	public interface IPostable : IAuditable, ITaggable, ICommentable, ILinkable, IReferenceable, ISearchable
	{}
}