using System;
using System.Collections.Generic;

namespace funda.common.auditing
{
    public interface IAuditable
    {
        List<IAuditEntry> AuditEntries { get; set; }
    }
}