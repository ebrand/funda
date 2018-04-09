using System;
using System.Collections.Generic;

namespace funda.common.auditing
{
	public interface IAuditable
    {
		int Identifier { get; set; }
		bool DeleteFlag { get; set; }
        List<AuditEntry> AuditEntries { get; set; }
    }
}