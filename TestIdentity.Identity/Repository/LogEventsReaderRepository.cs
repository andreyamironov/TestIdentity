using AMir.Interface.Data;
using AMir.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Data;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.Repository
{
    public class LogEventsReaderRepository : IReaderRepository<TestIdentity.Identity.Models.LogEvent, int>
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

        public IEnumerable<LogEvent> GetList(Func<LogEvent, object> orderByKeySelector, Func<LogEvent, bool> whereKeySelector, int skip, int take, out int total, string orderByAscDesc)
        {
            return _applicationEventDbContext.Events.SkipTakeFuncPredicate(orderByKeySelector,whereKeySelector, skip, take, out total, orderByAscDesc);
        }

        public int IndexOf(Func<LogEvent, bool> predicate, LogEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}
