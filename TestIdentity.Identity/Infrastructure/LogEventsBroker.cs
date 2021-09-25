using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;
using TestIdentity.Identity.Data;

namespace TestIdentity.Identity.Infrastructure
{
    public class LogEventsBroker
    {
        IWebHostEnvironment _appEnvironment;
        ApplicationEventDbContext _applicationEventDbContext;
        public LogEventsBroker(IWebHostEnvironment appEnvironment, ApplicationEventDbContext applicationEventDbContext)
        {
            _appEnvironment = appEnvironment;
            _applicationEventDbContext = applicationEventDbContext;

        }
        public LogEventsViewModel Get(TestIdentity.Identity.Core.HttpParams httpParams, string selectedId = "")
        {
            LogEventsViewModel pagerListModel = new LogEventsViewModel();
            pagerListModel.TotalCount = string.IsNullOrWhiteSpace(httpParams.Search)
                ? _applicationEventDbContext.Events.Count()
                : _applicationEventDbContext.Events.Count(u => u.Message.IndexOf(httpParams.Search) > -1);
            pagerListModel.ItemsPerPage = (int)(httpParams.Count > 0 ? httpParams.Count : 10);

            if (pagerListModel.TotalCount <= pagerListModel.ItemsPerPage)
            {
                pagerListModel.Page = 1;
            }
            else 
            {
                pagerListModel.Page = (int)(httpParams.Page > 0 ? httpParams.Page : 1);
            }


            pagerListModel.Search = httpParams.Search;
            pagerListModel.QueryString = httpParams.QueryString;

            var events = _applicationEventDbContext.Events.OrderByDescending(u=>u.Id)
                .Where(u => string.IsNullOrWhiteSpace(pagerListModel.Search) ? true : u.Message.Contains(pagerListModel.Search))
                .Skip((pagerListModel.Page - 1) * pagerListModel.ItemsPerPage)
                .Take(pagerListModel.ItemsPerPage);

            List<LogEventViewModel> items = new List<LogEventViewModel>();
            
            foreach(var i in events)
            {
                items.Add(new LogEventViewModel()
                {
                    TimeStamp = i.TimeStamp,
                    Level = i.Level,
                    Message = i.Message,
                    Exception = i.Exception,
                    Properties = i.Properties
                }) ;
            }

            pagerListModel.Items = items;

            return pagerListModel;
        }
    }
}
