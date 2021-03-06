using AMir.Interface.Data;
using AMir.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;

namespace IdentityServerAspNetIdentity.Repository
{
    public class LogEventsReaderRepository : IReaderRepository<IdentityServerAspNetIdentity.Models.LogEvent, int>
    {
        ApplicationEventDbContext _applicationEventDbContext;

        public LogEventsReaderRepository(ApplicationEventDbContext applicationEventDbContext)
        {
            _applicationEventDbContext = applicationEventDbContext;
        }

        public LogEvent Get(Func<LogEvent, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogEvent> GetList(Func<LogEvent, bool> predicate, int skip, int take, out int total)
        {
            return _applicationEventDbContext.Events.OrderByDescending(l=>l.Id).SkipTakeFuncPredicate(predicate, skip, take, out total);
        }

        public int IndexOf(Func<LogEvent, bool> predicate, LogEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}
