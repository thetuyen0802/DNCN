using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditLog
    {
        public Guid AuditLogId { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string EntityType {  get; set; } 
        public Guid EntityId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IpAddress {  get; set; }  
        public string UserAgent { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTimeOffset CreateAt { get; set; }

        public AuditLog()
        {

        }
    }
}
