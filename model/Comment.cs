using System;
using Newtonsoft.Json;

namespace funda.model
{
	public class Comment
	{
		[JsonProperty("commented-on")]
		public DateTime CommentedOn { get; set; }

		[JsonProperty("author")]
		public string Author { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("like-count")]
		public int LikeCount { get; set; }

		[JsonProperty("dislike-count")]
		public int DislikeCount { get; set; }

		public Comment()
		{}
	}
}