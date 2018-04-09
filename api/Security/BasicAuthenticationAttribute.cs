using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace funda.api.Security
{
	public class BasicAuthenticationAttribute : ActionFilterAttribute
	{

		private const string AUTHORIZATION_HEADER = "Authorization";
		private const string AUTHORIZATION_TYPE = "Basic";
		private const string ENCODING = "iso-8859-1";

		private static readonly string USERNAME = Environment.GetEnvironmentVariable("API_USERNAME");
		private static readonly string PASSWORD = Environment.GetEnvironmentVariable("API_PASSWORD");

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<BasicAuthenticationAttribute>)) as ILogger;

			string username = "";
			string password = "";

			string authHeader = context.HttpContext.Request.Headers[AUTHORIZATION_HEADER];
			if (authHeader != null && authHeader.StartsWith(AUTHORIZATION_TYPE))
			{
				// Extract credentials
				try
				{
					string encodedUsernamePassword = authHeader.Substring(AUTHORIZATION_TYPE.Length + 1).Trim();
					Encoding encoding = Encoding.GetEncoding(ENCODING);
					string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

					int seperatorIndex = usernamePassword.IndexOf(':');

					username = usernamePassword.Substring(0, seperatorIndex);
					password = usernamePassword.Substring(seperatorIndex + 1);

					// Validate credentials
					if (username == USERNAME && password == PASSWORD)
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
