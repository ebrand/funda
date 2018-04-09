using System;
using System.Threading.Tasks;
using funda.model;
using funda.repository.strategies;
using System.Collections.Generic;
using funda.common.logging;
using Bogus;
using funda.common.auditing;
using System.Linq;
using Bogus.Extensions;
using System.Diagnostics;

namespace funda.repository.fake
{
	public class FakeAsyncRepository_Post : IAsyncRepository<Post>
	{
		private List<Post> _collection;
		private readonly IFundaLogger<Post> _logger;
		public IStrategyFactory<Post> StrategyFactory { get; private set; }

		public Task<AsyncResponse<Post>> CreateAsync(Post obj)
		{
			throw new NotImplementedException();
		}

		public async Task<AsyncResponse<Post>> ReadAsync(int id)
		{
			try
			{
				_logger.LogInfo(Events.Repository.Read.InProgress, $"Retrieving object ID:{id.ToString()}...");
				return await this.StrategyFactory.Read.ReadAsync(id, _collection);
			}
			catch(Exception exc)
			{
				_logger.LogError(
					eventId: Events.Repository.Read.Failure,
					message: $"Failed to retrieve object {id.ToString()}.",
					exception: exc
				);
				throw;
			}
		}

		public Task<AsyncResponse<Post>> UpdateAsync(Post obj)
		{
			throw new NotImplementedException();
		}

		public Task<AsyncResponse<Post>> DeleteAsync(Post obj)
		{
			throw new NotImplementedException();
		}

		public FakeAsyncRepository_Post(IStrategyFactory<Post> strategyFactory, IFundaLogger<Post> logger)
		{
			_logger = logger;
			this.StrategyFactory = strategyFactory;
		}

		public void Initialize()
		{
			// create a bunch of fake posts
			if (_collection == null)
			{
				var sw = new Stopwatch();
				sw.Start();
				_collection = new List<Post>();
				var commentFaker = new Faker<Comment>();
				commentFaker
					.RuleFor(c => c.Author, f => f.Person.FullName)
					.RuleFor(c => c.BodyMarkdown, f => f.Rant.Review())
					.RuleFor(c => c.CommentedOn, f => f.Date.Past(2))
					.RuleFor(c => c.DislikeCount, f => f.Random.Int(0, 3))
					.RuleFor(c => c.LikeCount, f => f.Random.Int(2, 8));

				var referenceFaker = new Faker<Reference>();
				referenceFaker
					.RuleFor(r => r.Name, f => f.Lorem.Sentence())
					.RuleFor(r => r.Url, f => f.Internet.Url());

				var auditFaker = new Faker<AuditEntry>();
				auditFaker
					.RuleFor(a => a.AuditActor, f => f.Person.FullName)
					.RuleFor(a => a.AuditDateTime, f => f.Date.Past(2))
					.RuleFor(a => a.AuditMessage, f => f.Lorem.Sentence())
					.RuleFor(a => a.AuditType, f => f.Random.Enum<AuditType>());

				var postFaker = new Faker<Post>();
				postFaker
					.RuleFor(p => p.Identifier, f => f.IndexFaker)
					.RuleFor(p => p.DeleteFlag, false)
					.RuleFor(p => p.AllowComments, true)
					.RuleFor(p => p.DisplayReferences, true)
					.RuleFor(p => p.PermaLink, f => f.Internet.Url())
					.RuleFor(p => p.Title, f => f.Lorem.Sentence(5))
					.RuleFor(p => p.Preamble, f => f.Lorem.Paragraphs(1, 2))
					.RuleFor(p => p.BodyMarkdown, f => f.Lorem.Paragraphs(6, 10))
					.RuleFor(p => p.Author, f => f.Person.FullName)
					.RuleFor(p => p.PublishDateTime, f => f.Date.Past(2))
					.RuleFor(p => p.Tags, f => f.Random.WordsArray(2, 5).ToList())
					.RuleFor(p => p.Comments, f => commentFaker.GenerateBetween(2, 10))
					.RuleFor(p => p.References, f => referenceFaker.GenerateBetween(0, 3))
					.RuleFor(p => p.AuditEntries, f => auditFaker.GenerateBetween(0, 2));

				_collection = postFaker.Generate(1000);

				sw.Stop();

				Console.WriteLine($"Fake posts initialized in {sw.ElapsedMilliseconds.ToString()} ms.");
			}
		}
	}
}