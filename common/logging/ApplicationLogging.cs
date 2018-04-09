using Microsoft.Extensions.Logging;
using System;

namespace funda.common.logging
{
	public static class ApplicationLogging
	{
		private static ILoggerFactory _factory = null;

		public static void ConfigureLogger(ILoggerFactory factory)
		{}

		public static ILoggerFactory LoggerFactory
		{
			get
			{
				if (_factory == null)
				{
					_factory = new LoggerFactory();
					ConfigureLogger(_factory);
				}
				return _factory;
			}
			set { _factory = value; }
		}
		public static ILogger<T> CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
	}
}