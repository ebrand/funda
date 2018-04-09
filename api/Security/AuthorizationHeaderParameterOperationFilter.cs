using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace funda.api.Security
{
	public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
			var isAuthRequired = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is BasicAuthenticationAttribute);

			if (isAuthRequired)
			{
				if (operation.Parameters == null)
					operation.Parameters = new List<IParameter>();

				operation.Parameters.Add(new NonBodyParameter
				{
					Name = "Authorization",
					In = "header",
					Description = "Basic authentication required",
					Required = true,
					Type = "string"
				});
			}
		}
	}
}
