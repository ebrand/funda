﻿using System;
using System.Collections.Generic;
using funda.common.auditing;
using HeyRed.MarkdownSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using funda.model.fakes;

namespace funda.model
{
	public class Post : IPostable
	{
		// ID, flags, ald permalink
		[JsonProperty("identifier")]
		public int Identifier { get; set; }

		[JsonProperty("delete-flag")]
		public bool DeleteFlag { get; set; }

		[JsonProperty("allow-comments")]
		public bool AllowComments { get; set; }

		[JsonProperty("display-references")]
		public bool DisplayReferences { get; set; }

		[JsonProperty("permalink")]
		public string PermaLink { get; set; }

		// post content
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("preamble")]
		public string Preamble { get; set; }

		[JsonProperty("body-as-markdown")]
		public string BodyAsMarkdown { get; set; }

		[JsonProperty("body-as-html")]
		public string BodyAsHtml => new Markdown().Transform(this.BodyAsMarkdown);

		[JsonProperty("author")]
		public string Author { get; set; }

		[JsonProperty("publish-date-time")]
		public DateTime PublishDateTime { get; set; }

		// collections
		[JsonProperty("tags")]
		public List<string> Tags { get; set; }

		[JsonProperty("comments")]
		public List<Comment> Comments { get; set; }

		[JsonProperty("references")]
		public List<Reference> References { get; set; }

		[JsonProperty("audit-entries")]
		public List<AuditEntry> AuditEntries { get; set; }

		public Post()
		{}
		public Post(int identifier, string author, bool allowComments = true, bool displayReferences = true)
		{
			this.Identifier 	   = identifier;
			this.DeleteFlag 	   = false;
			this.AllowComments	   = allowComments;
			this.DisplayReferences = displayReferences;
			this.Author 		   = author;
			this.Tags 			   = new List<string>();
			this.Comments 		   = new List<Comment>();
			this.References 	   = new List<Reference>();
			this.AuditEntries 	   = new List<AuditEntry>();
		}

		public bool ContainsSearchTerm(string searchTerm)
		{
			return
				   this.AuditEntries.Any(ae => ae.ContainsSearchTerm(searchTerm))
				|| this.Author.Contains(searchTerm)
				|| this.BodyAsMarkdown.Contains(searchTerm)
				|| this.Comments.Any(c => c.ContainsSearchTerm(searchTerm))
				|| this.Preamble.Contains(searchTerm)
				|| this.References.Any(r => r.ContainsSearchTerm(searchTerm))
				|| this.Tags.Any(t => t.Contains(searchTerm))
				|| this.Title.Contains(searchTerm);
		}

		public static Post SamplePost
		{
			get { return FakeFactory.PostFaker.Generate(); }
		}
	}
}