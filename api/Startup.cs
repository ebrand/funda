using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using funda.common;
using funda.repository;
using funda.repository.mongo;
using funda.model;
using SimpleInjector;
using funda.repository.strategies;
using funda.repository.mongo.strategies;

namespace api
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

			// ### SimpleInjector DI container
			_siContainer.Register(typeof(IAsyncRepository<>), typeof(MongoAsyncRepository<>));
			_siContainer.Register(typeof(IStrategyFactory<>), typeof(StrategyFactory<>));

			// ### These feed specific strategies into the above strategy factory (create, read, update, and delete)
			_siContainer.Register(typeof(ICreateStrategy<>), typeof(MongoCreateStrategy_Normal<>));
			_siContainer.Register(typeof(IReadStrategy<>),   typeof(MongoReadStrategy_Normal<>));
			_siContainer.Register(typeof(IUpdateStrategy<>), typeof(MongoUpdateStrategy_CreatesNewRevision<>));
			//_siContainer.Register(typeof(IDeleteStrategy<>), typeof(MongoDeleteStrategy_Normal<>));
			_siContainer.Register(typeof(IDeleteStrategy<>), typeof(MongoDeleteStrategy_MarkForDeletion<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
