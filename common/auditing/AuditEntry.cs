using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace funda.common.auditing
{
	public class AuditEntry : ISearchable
    {
        public DateTime AuditDateTime { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public AuditType AuditType { get; set; }
        public string AuditMessage { get; set; }
        public string AuditActor { get; set; }

		public AuditEntry()
		{}
		public AuditEntry(DateTime dateTime, AuditType type, string message, string actor)
        {
            this.AuditDateTime = dateTime;
            this.AuditType = type;
            this.AuditMessage = message;
            this.AuditActor = actor;
        }

		public bool ContainsSearchTerm(string searchTerm)
		{
			return
					this.AuditActor.Contains(searchTerm)
				||  this.AuditMessage.Contains(searchTerm)
				||  this.AuditType.ToString().Contains(searchTerm);
		}
	}
}