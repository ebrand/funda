using System;
using Xunit;
using SimpleInjector;
using funda.model;
using funda.repository.strategies;
using funda.repository;
using funda.repository.mongo;
using funda.repository.mongo.strategies;

namespace funda.tests
{
	public class UnitTest1
	{
		private readonly Container _siContainer = new Container();

		[Fact]
		public void SimpleInjectorWorks()
		{
			// ARRANGE
			// ### SimpleInjector DI container
			_siContainer.Register(typeof(IAsyncRepository<>), typeof(MongoAsyncRepository<>));

			// ### These feed a specific strategy factory and CRUD strategies into the above strategy factory
			_siContainer.Register(typeof(IAsyncRepositoryStrategyFactory<>), typeof(MongoStrategyFactory<>));
			_siContainer.Register(typeof(ICreateStrategy<>), typeof(MongoCreateStrategy_Normal<>));
			_siContainer.Register(typeof(IReadStrategy<>), typeof(MongoReadStrategy_Normal<>));
			_siContainer.Register(typeof(IUpdateStrategy<>), typeof(MongoUpdateStrategy_CreatesNewRevision<>));
			_siContainer.Register(typeof(IDeleteStrategy<>), typeof(MongoDeleteStrategy_MarkForDeletion<>));

			// ACT
			var repository = _siContainer.GetInstance<IAsyncRepository<Post>>();

			// ASSERT
			Assert.IsType<MongoAsyncRepository<Post>>(repository);
			Assert.IsType<MongoStrategyFactory<Post>>(repository.StrategyFactory);
			Assert.IsType<MongoCreateStrategy_Normal<Post>>(repository.StrategyFactory.Create);
			Assert.IsType<MongoReadStrategy_Normal<Post>>(repository.StrategyFactory.Read);
			Assert.IsType<MongoUpdateStrategy_CreatesNewRevision<Post>>(repository.StrategyFactory.Update);
			Assert.IsType<MongoDeleteStrategy_MarkForDeletion<Post>>(repository.StrategyFactory.Delete);
		}
	}
}
