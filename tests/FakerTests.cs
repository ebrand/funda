﻿using System;
using Xunit;
using SimpleInjector;
using funda.model;
using funda.repository.strategies;
using funda.repository;
using funda.repository.mongo;
using funda.repository.mongo.strategies;
using funda.common.logging;
using funda.repository.fake;
using funda.repository.fake.strategies;

namespace funda.tests
{
	public class FakerTests
	{
		private readonly Container _siContainer = new Container();

		[Fact]
		public async void FakerWorks()
		{
			// ARRANGE
			// ### SimpleInjector DI container
			_siContainer.Register(typeof(IFundaLogger<>), typeof(MicrosoftLogger<>));
			_siContainer.Register(typeof(IAsyncRepository<>), typeof(FakeAsyncRepository));
			_siContainer.Register(typeof(IStrategyFactory<>), typeof(StrategyFactory<>));

			// ### These feed a specific strategy factory and CRUD strategies into the above strategy factory
			_siContainer.Register(typeof(ICreateStrategy<>), typeof(FakeCreateStrategy_NotImplemented<>));
			_siContainer.Register(typeof(IReadStrategy<>),   typeof(FakeReadStrategy_Normal<>));
			_siContainer.Register(typeof(IUpdateStrategy<>), typeof(FakeUpdateStrategy_NotImplemented<>));
			_siContainer.Register(typeof(IDeleteStrategy<>), typeof(FakeDeleteStrategy_NotImplemented<>));

			// ACT
			var repository = _siContainer.GetInstance<IAsyncRepository<Post>>();
			repository.Initialize();
			var response = await repository.ReadAsync(1);
			var post = response.Payload;

			// ASSERT
			Assert.NotNull(post);
			Assert.NotEmpty(post.Title);
		}
	}
}