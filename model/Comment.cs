using System;
using funda.common;
using HeyRed.MarkdownSharp;
using Newtonsoft.Json;

namespace funda.model
{
	public class Comment : ISearchable
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

		public bool ContainsSearchTerm(string searchTerm)
		{
			return
				   this.Author.Contains(searchTerm)
				|| this.BodyAsMarkdown.Contains(searchTerm);

		}
	}
}