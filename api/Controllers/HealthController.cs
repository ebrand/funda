using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace funda.api.Controllers
{
	[Produces("application/json")]
	[Route("api/health")]
	public class HealthController : Controller
	{
		[HttpGet]
		public IActionResult Get()
		{
			var envVars = Environment.GetEnvironmentVariables();
			var envs = new SortedList();
			foreach(var e in envVars.Keys)
			{
				var es = e as string;
				if(es.Contains("FUNDA_"))
				{
					if(es.ToLower().Contains("password"))
						envs.Add(es, "*REDACTED*");
					else
						envs.Add(es, Environment.GetEnvironmentVariable(es));
				}
			}
			return Ok(envs);
		}
	}
}