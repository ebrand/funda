using System;
using HeyRed.MarkdownSharp;
using Newtonsoft.Json;

namespace funda.cli.viewmodels
{
	public class Comment
	{
		[JsonProperty("commented-on")]
		public DateTime CommentedOn { get; set; }

		[JsonProperty("author")]
		public string Author { get; set; }

		[JsonProperty("body-as-markdown")]
		public string BodyAsMarkdown { get; set; }

		[JsonProperty("body-as-html")]
		public string BodyAsHtml => new Markdown().Transform(this.BodyAsMarkdown);

		[JsonProperty("like-count")]
		public int LikeCount { get; set; }

		[JsonProperty("dislike-count")]
		public int DislikeCount { get; set; }

		public Comment()
		{}
	}
}