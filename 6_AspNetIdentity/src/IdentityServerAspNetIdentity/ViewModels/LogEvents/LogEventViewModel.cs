using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class LogEventViewModel
    {
        public DateTime? TimeStamp { get; set; }        
        public string Level { get; set; }
        public string Message { get; set; }    
        public string Exception { get; set; }
        public string Properties { get; set; }
    }
}
