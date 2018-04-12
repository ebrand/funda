using System;
using System.Text;
using funda.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace funda.api.security
{
	public class BasicAuthenticationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<BasicAuthenticationAttribute>)) as ILogger;

			string username = "";
			string password = "";

			string authHeader = context.HttpContext.Request.Headers[Utilities.Configuration.AuthHeaderName];
			if (authHeader != null && authHeader.StartsWith(Utilities.Configuration.AuthType))
			{
				// Extract credentials
				try
				{
					string encodedUsernamePassword = authHeader.Substring(Utilities.Configuration.AuthType.Length + 1).Trim();
					Encoding encoding = Encoding.GetEncoding(Utilities.Configuration.Encoding);
					string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

					int seperatorIndex = usernamePassword.IndexOf(':');

					username = usernamePassword.Substring(0, seperatorIndex);
					password = usernamePassword.Substring(seperatorIndex + 1);

					// Validate credentials
					if (username == Utilities.Configuration.ApiUserName && password == Utilities.Configuration.ApiPassword)
					{
						logger.LogInformation(username + " successfully authenticated");
						return;
					}
				}
				catch { ; }
			}

			logger.LogWarning("request was not successfully authenticated. Username = " + username);
			context.Result = new StatusCodeResult(401);
		}
	}
}