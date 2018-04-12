using System;
using funda.common.auditing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace funda.common
{
    public static class Utilities
    {
		public static class Configuration
		{
			public static class KeyNames
			{
				private static string _useFakeData	  = "FUNDA_USE_FAKE_DATA";
				private static string _apiUsername	  = "FUNDA_API_USERNAME";
				private static string _apiPassword	  = "FUNDA_API_PASSWORD";
				private static string _logLevel		  = "FUNDA_LOG_LEVEL";
				private static string _authheaderName = "FUNDA_AUTH_HEADER_NAME";
				private static string _authType		  = "FUNDA_AUTH_TYPE";
				private static string _encoding		  = "FUNDA_ENCODING";

				public static string USE_FAKE_DATA { get => _useFakeData; }
				public static string API_USERNAME { get => _apiUsername; }
				public static string API_PASSWORD { get => _apiPassword; }
				public static string LOG_LEVEL { get => _logLevel; }
				public static string AUTH_HEADER_NAME { get => _authheaderName; }
				public static string AUTH_TYPE { get => _authType; }
				public static string ENCODING { get => _encoding; }
			}

			public static bool UseFakeData
			{
				get
				{
					var useFakeData = true;
					var useFakeDataFromEnv = Environment.GetEnvironmentVariable(KeyNames.USE_FAKE_DATA);

					if (!string.IsNullOrEmpty(useFakeDataFromEnv))
						bool.TryParse(useFakeDataFromEnv, out useFakeData);

					return useFakeData;
				}
			}
			public static string ApiUserName { get => Environment.GetEnvironmentVariable(KeyNames.API_USERNAME) ?? string.Empty; }
			public static string ApiPassword { get => Environment.GetEnvironmentVariable(KeyNames.API_PASSWORD) ?? string.Empty; }
			public static LogLevel LogLevel
			{
				get
				{
					var defaultLogLevel = LogLevel.Trace;
					var logLevelFromEnv = Environment.GetEnvironmentVariable(KeyNames.LOG_LEVEL);
					if (!string.IsNullOrEmpty(logLevelFromEnv))
						Enum.TryParse(logLevelFromEnv, true, out defaultLogLevel);

					return defaultLogLevel;
				}
			}
			public static string AuthHeaderName { get => Environment.GetEnvironmentVariable(KeyNames.AUTH_HEADER_NAME) ?? string.Empty; }
			public static string AuthType { get => Environment.GetEnvironmentVariable(KeyNames.AUTH_TYPE) ?? string.Empty; }
			public static string Encoding { get => Environment.GetEnvironmentVariable(KeyNames.ENCODING) ?? string.Empty; }
		}

		public static class Auditing
		{
			public static void AddCreateAudit(IAuditable auditable)
			{
				auditable.AuditEntries.Add(
					new AuditEntry(
						DateTime.UtcNow,
						AuditType.Create,
						$"{auditable.Identifier.ToString()} created.",
						""
					)
				);
			}
			public static void AddUpdateAudit(IAuditable auditable)
			{
				auditable.AuditEntries.Add(
					new AuditEntry(
						DateTime.UtcNow,
						AuditType.Update,
						$"{auditable.Identifier.ToString()} updated.",
						""
					)
				);
			}
			public static void AddDeleteAudit(IAuditable auditable)
			{
				auditable.AuditEntries.Add(
					new AuditEntry(
						DateTime.UtcNow,
						AuditType.Delete,
						$"{auditable.Identifier.ToString()} deleted.",
						""
					)
				);
			}
		}
        
		public static string SerializeToJson<T>(T obj)
		{
			var result = JsonConvert.SerializeObject(obj, Formatting.Indented);
			return result;
		}
    }
}