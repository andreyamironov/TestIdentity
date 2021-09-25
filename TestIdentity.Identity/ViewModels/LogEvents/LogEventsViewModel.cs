using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;

namespace TestIdentity.Identity.ViewModels
{
    public class LogEventsViewModel: PagerListModel<LogEventViewModel>
    {
        public LogEventsViewModel()
        {
            base.ControllerName = "LogEvents";
        }

        public LogEventsViewModel(HttpParams httpParams, int total, IEnumerable<LogEventViewModel> items) : base(httpParams, total, items)
        {
            base.ControllerName = "LogEvents";
        }
    }
}
