using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.Mapping
{
    public class LogEventMapperConfiguration : MapperConfigurationBase
    {
        public LogEventMapperConfiguration()
        {
            CreateMap<LogEvent, LogEventViewModel>();
        }
    }
}
