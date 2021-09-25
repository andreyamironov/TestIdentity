using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public abstract class PagerListModel<T>
    {
        public Dictionary<string, object> Informations = new Dictionary<string, object>();

        public string ControllerName {  get; protected set; }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalCount { get; set; }
        public string Search { get; set; }
        public string Tag { get; set; }

        string queryString;
        public string QueryString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(queryString)) return queryString;
                else return $"/{ControllerName}";
            }
            set
            {
                queryString = value;
            }
        }


        public IEnumerable<T> Items { get; set; }
        public T SelectedItem { get; set;}
        public PagerListModel()
        {
                
        }
        public PagerListModel(HttpParams httpParams, int total,IEnumerable<T> items)
        {
            TotalCount      = total;
            Page            =(int) httpParams.Page;
            ItemsPerPage    = (int)httpParams.Count;
            Search          = httpParams.Search;
            QueryString     = httpParams.QueryString;

            Items = items;
        }
    }
}
