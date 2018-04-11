using System;
using funda.api.security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace funda.api
{
	public partial class Startup
	{
		public void ConfigureServices_Swagger(IServiceCollection services)
		{
			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Funda.Api", Version = "v1" });

				// Add operation filter that tells swagger UI to ask for authentication when submitting a request.
				c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
			});
		}

		public void Configure_Swagger(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funda.Api V1");
			});
		}
	}
}
