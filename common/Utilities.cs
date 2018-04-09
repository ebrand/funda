using System;
using funda.common.auditing;

namespace funda.common
{
    public static class Utilities
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
}