using System;
using System.Linq;
using Bogus;
using Bogus.Extensions;
using funda.common.auditing;

namespace funda.model.fakes
{
    public static class FakeFactory
    {
		public static Faker<Comment> CommentFaker =>
			new Faker<Comment>()
				.RuleFor(c => c.Author, f => f.Person.FullName)
				.RuleFor(c => c.BodyAsMarkdown, f => f.Rant.Review())
				.RuleFor(c => c.CommentedOn, f => f.Date.Past(2))
				.RuleFor(c => c.DislikeCount, f => f.Random.Int(0, 3))
				.RuleFor(c => c.LikeCount, f => f.Random.Int(2, 8));

		public static Faker<Reference> ReferenceFaker =>
			new Faker<Reference>()
				.RuleFor(r => r.Name, f => f.Lorem.Sentence())
				.RuleFor(r => r.Url, f => f.Internet.Url());

		public static Faker<AuditEntry> AuditEntryFaker =>
			new Faker<AuditEntry>()
				.RuleFor(a => a.AuditActor, f => f.Person.FullName)
				.RuleFor(a => a.AuditDateTime, f => f.Date.Past(2))
				.RuleFor(a => a.AuditMessage, f => f.Lorem.Sentence())
				.RuleFor(a => a.AuditType, f => f.Random.Enum<AuditType>());

		public static Faker<Post> PostFaker =>
			new Faker<Post>()
				.RuleFor(p => p.Identifier, f => f.IndexFaker)
				.RuleFor(p => p.DeleteFlag, false)
				.RuleFor(p => p.AllowComments, true)
				.RuleFor(p => p.DisplayReferences, true)
				.RuleFor(p => p.PermaLink, f => f.Internet.Url())
				.RuleFor(p => p.Title, f => f.Lorem.Sentence(5))
				.RuleFor(p => p.Preamble, f => f.Lorem.Paragraphs(1, 2))
				.RuleFor(p => p.BodyAsMarkdown, f => f.Lorem.Paragraphs(6, 10))
				.RuleFor(p => p.Author, f => f.Person.FullName)
				.RuleFor(p => p.PublishDateTime, f => f.Date.Past(2))
				.RuleFor(p => p.Tags, f => f.Random.WordsArray(2, 5).ToList())
				.RuleFor(p => p.Comments, f => FakeFactory.CommentFaker.GenerateBetween(2, 10))
				.RuleFor(p => p.References, f => FakeFactory.ReferenceFaker.GenerateBetween(0, 3))
				.RuleFor(p => p.AuditEntries, f => FakeFactory.AuditEntryFaker.GenerateBetween(0, 2));
	}
}