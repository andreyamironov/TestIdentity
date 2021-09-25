using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.Mapping
{
    public class LogEventMapperConfiguration : MapperConfigurationBase
    {
        public LogEventMapperConfiguration()
        {
            CreateMap<LogEvent, LogEventViewModel>();
        }
    }
}
