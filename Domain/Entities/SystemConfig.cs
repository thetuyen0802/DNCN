using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Domain.Entities
{
    public class SystemConfig
    {
        public string ConfigId { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Decreiption { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset UpdateAt { get; set; }
        public SystemConfig() { }

    }
}
