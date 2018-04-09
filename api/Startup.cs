using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using funda.repository;
using funda.repository.fake;
using funda.repository.fake.strategies;
using funda.repository.mongo;
using SimpleInjector;
using funda.repository.strategies;
using funda.repository.mongo.strategies;
using funda.common.logging;
using Microsoft.AspNetCore.Http;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Swashbuckle.AspNetCore.Swagger;
using funda.api.security;

namespace funda.api
{
    public class Startup
    {
		private readonly Container _siContainer = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Funda.Api", Version = "v1" });

				// Add operation filter that tells swagger UI to ask for authentication when submitting a request.
				c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
			});

			// integrate SimpleInjector with the ASP.Net pipeline
			_siContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_siContainer));
			services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_siContainer));
			services.EnableSimpleInjectorCrossWiring(_siContainer);
			services.UseSimpleInjectorAspNetRequestScoping(_siContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			ApplicationLogging.LoggerFactory = loggerFactory;

			// ### SimpleInjector DI container
			_siContainer.RegisterSingleton(typeof(IFundaLogger<>), typeof(FundaMicrosoftLogger<>));
			_siContainer.RegisterSingleton(typeof(IStrategyFactory<>), typeof(StrategyFactory<>));

			// ### These feed specific strategies into the above strategy factory (create, read, update, and delete)
			var useFakeData = true;
			if (bool.TryParse(Environment.GetEnvironmentVariable("USE_FAKE_DATA").ToLower(), out useFakeData) && useFakeData)
			{
				_siContainer.RegisterSingleton(typeof(IAsyncRepository<>),  typeof(FakeAsyncRepository_Post));
				_siContainer.RegisterSingleton(typeof(ICreateStrategy<>),   typeof(FakeCreateStrategy_NotImplemented<>));
				_siContainer.RegisterSingleton(typeof(IReadAllStrategy<>),  typeof(FakeReadAllStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IReadStrategy<>),		typeof(FakeReadStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IUpdateStrategy<>),   typeof(FakeUpdateStrategy_NotImplemented<>));
				_siContainer.RegisterSingleton(typeof(IDeleteStrategy<>),   typeof(FakeDeleteStrategy_NotImplemented<>));
			}
			else
			{
				_siContainer.RegisterSingleton(typeof(IAsyncRepository<>),  typeof(MongoAsyncRepository<>));
				_siContainer.RegisterSingleton(typeof(ICreateStrategy<>),   typeof(MongoCreateStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IReadStrategy<>),     typeof(MongoReadStrategy_Normal<>));
				_siContainer.RegisterSingleton(typeof(IUpdateStrategy<>),   typeof(MongoUpdateStrategy_CreatesNewRevision<>));
				_siContainer.RegisterSingleton(typeof(IDeleteStrategy<>),   typeof(MongoDeleteStrategy_MarkForDeletion<>));
			}

			_siContainer.Verify();

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funda.Api V1");
			});
		}
    }
}