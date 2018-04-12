using System;
using funda.common.logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace funda.api
{
    public partial class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
			services.AddRouting();

			ConfigureServices_Swagger(services);
			ConfigureServices_SimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			ApplicationLogging.LoggerFactory = loggerFactory;

			if(env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseMvc(routes => {
				routes.MapRoute("ReadAllPosts",      "api/{controller=Posts}/{action=ReadAllAsync}");
				routes.MapRoute("ReadOnePost",       "api/{controller=Posts}/{action=ReadAsync}/{id}");
				routes.MapRoute("ReadLatestPosts",   "api/{controller=Posts}/{action=ReadLatestAsync}");
				routes.MapRoute("PostKeywordSearch", "api/{controller=Search}/{action=PostsKeywordSearchAsync}/{q}");
				routes.MapRoute("Health",			 "api/{controller=Health}/{action=GetHealth}");
			});

			Configure_Swagger(app);
			Configure_SimpleInjector();
		}
    }
}