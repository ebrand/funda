using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

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
			var logLevel = LogLevel.Trace;
			var logLevelFromEnv = Environment.GetEnvironmentVariable("LOG_LEVEL");

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