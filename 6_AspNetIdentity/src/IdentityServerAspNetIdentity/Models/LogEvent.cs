using System;
using System.Collections.Generic;

#nullable disable

namespace IdentityServerAspNetIdentity.Models
{
    public partial class LogEvent
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
    }
}
