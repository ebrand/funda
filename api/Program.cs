using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace funda.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

		public static IWebHost BuildWebHost(string[] args)
		{
			new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();
			
			var logLevel = LogLevel.Trace;
			var logLevelFromEnv = Environment.GetEnvironmentVariable("FUNDA_LOG_LEVEL");

			if (!string.IsNullOrEmpty(logLevelFromEnv))
				Enum.TryParse(logLevelFromEnv, true, out logLevel);
			
			var host = new WebHostBuilder()
                .UseApplicationInsights()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureLogging((hostingContext, logging) => { logging.AddConsole().SetMinimumLevel(logLevel); })
				.UseStartup<Startup>()
				.Build();
			return host;
		}
	}
}