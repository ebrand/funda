using System;
using funda.common;
using funda.common.logging;
using funda.repository;
using funda.repository.fake;
using funda.repository.fake.strategies;
using funda.repository.mongo;
using funda.repository.mongo.strategies;
using funda.repository.strategies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace funda.api
{
	public partial class Startup
	{
		private readonly Container _siContainer = new Container();

		public void ConfigureServices_SimpleInjector(IServiceCollection services)
		{
			// integrate SimpleInjector with the ASP.Net pipeline
			_siContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_siContainer));
			services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_siContainer));
			services.EnableSimpleInjectorCrossWiring(_siContainer);
			services.UseSimpleInjectorAspNetRequestScoping(_siContainer);
		}

		public void Configure_SimpleInjector()
		{
			// ### SimpleInjector DI container
			_siContainer.RegisterSingleton(typeof(IFundaLogger<>), typeof(FundaMicrosoftLogger<>));
			_siContainer.RegisterSingleton(typeof(IStrategyFactory<>), typeof(StrategyFactory<>));
			_siContainer.RegisterSingleton(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

			// ### These feed specific strategies into the above strategy factory (create, read, update, and delete)
			if (Utilities.Configuration.UseFakeData)
			{
				_siContainer.RegisterSingleton(typeof(ICreateStrategy<>),  typeof(FakeCreateStrategy_NotImplemented<>));
				_siContainer.RegisterSingleton(typeof(IReadStrategy<>),    typeof(FakeReadStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IUpdateStrategy<>),  typeof(FakeUpdateStrategy_NotImplemented<>));
				_siContainer.RegisterSingleton(typeof(IDeleteStrategy<>),  typeof(FakeDeleteStrategy_NotImplemented<>));
				_siContainer.RegisterSingleton(typeof(ISearchStrategy<>),  typeof(FakeSearchStrategy_Normal<>));
			}
			else
			{
				_siContainer.RegisterSingleton(typeof(ICreateStrategy<>),  typeof(MongoCreateStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IReadStrategy<>),    typeof(MongoReadStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IUpdateStrategy<>),  typeof(MongoUpdateStrategy_CreatesNewRevision<>));
				_siContainer.RegisterSingleton(typeof(IDeleteStrategy<>),  typeof(MongoDeleteStrategy_MarkForDeletion<>));
				_siContainer.RegisterSingleton(typeof(ISearchStrategy<>),  typeof(MongoSearchStrategy_Normal<>));
			} 

			_siContainer.Verify();
		}
	}
}
