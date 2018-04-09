using System;
using funda.common.auditing;
using Newtonsoft.Json;

namespace funda.common
{
    public static class Utilities
    {
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