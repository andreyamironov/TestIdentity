using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Core
{
    public class HttpParams
    {
        const int DEFAULT_TAKE = 100;
        
        public int Page { get;  set; }
        public int Count { get;  set; }
        public string Search { get;  set; }
        public string Tag { get;  set; }        
        public dynamic SelectedId { get; set; }

        public string QueryStringFromProperties
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Page > 0) sb.Append($"{KeyWord.Page}={Page}");

                if (Count > 0)
                {
                    if (sb.Length > 0) sb.Append('&');
                    sb.Append($"{KeyWord.Count}={Count}"); 
                }

                if (!String.IsNullOrWhiteSpace(Search))
                {
                    if (sb.Length > 0) sb.Append('&');
                    sb.Append($"{KeyWord.Search}={Search}"); 
                }

                if (SelectedId != null)
                {
                    if (sb.Length > 0) sb.Append('&');
                    sb.Append($"{KeyWord.SelectedId}={SelectedId}");
                }

                if (sb.Length > 0) sb.Insert(0,'?');

                return sb.ToString();
            }
        }

        public string QueryString { get; set; }

        public int Total { get; set; }


        public static HttpParams Get(HttpContext httpContext)
        {
            HttpParams httpParams = new HttpParams();
            int iTmp;
            string sTmp = httpContext.Request.Query[KeyWord.Page];
            int.TryParse(sTmp, out iTmp);
            httpParams.Page = iTmp;

            sTmp = httpContext.Request.Query[KeyWord.Count];
            int.TryParse(sTmp, out iTmp);
            httpParams.Count = iTmp;

            httpParams.Search = httpContext.Request.Query[KeyWord.Search];

            httpParams.Tag = httpContext.Request.Query[KeyWord.Tag];

            httpParams.SelectedId = httpContext.Request.Query[KeyWord.SelectedId];

            httpParams.QueryString = $"{httpContext.Request.Path}{httpContext.Request.QueryString.Value}";
            return httpParams;
        }
        public static HttpParams Get(string url)
        {
            HttpParams httpParams = new HttpParams();
            if (!string.IsNullOrWhiteSpace(url))
            {
                int indexQuestion = url.IndexOf('?');
                if (indexQuestion > -1) httpParams.QueryString = url.Substring(indexQuestion);

                if (!string.IsNullOrWhiteSpace(httpParams.QueryString))
                {
                    string[] keys = httpParams.QueryString.Substring(1)?.Split("&");

                    int tmpInt;
                    foreach (string key in keys)
                    {
                        string[] subKeys = key.Split('=');
                        if (String.Compare(subKeys[0],KeyWord.Page,StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            tmpInt = 0;
                            int.TryParse(subKeys[1], out tmpInt);
                            httpParams.Page = tmpInt;
                        }
                        else if (String.Compare(subKeys[0],KeyWord.Count, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            tmpInt = 0;
                            int.TryParse(subKeys[1], out tmpInt);
                            httpParams.Count = tmpInt;
                        }
                        else if (String.Compare(subKeys[0],KeyWord.Search, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            httpParams.Search = subKeys[1];
                        }
                        else if (String.Compare(subKeys[0],KeyWord.Tag, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            httpParams.Tag = subKeys[1];
                        }
                        else if (String.Compare(subKeys[0], KeyWord.SelectedId, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            httpParams.SelectedId = subKeys[1];
                        }
                    }
                }
                else httpParams.QueryString = url;
            }

            return httpParams;
        }



        public static void  CalculateSkipTake(HttpParams httpParams,out int skip, out int take, int selectedIndex = 0)
        {
            if (httpParams.Count <= 0) httpParams.Count = take = DEFAULT_TAKE;
            else take = (int)httpParams.Count;

            if(selectedIndex > 0)
                httpParams.Page = (selectedIndex / take) + 1;

            if (httpParams.Page <= 0) skip = 0;
            else skip = ((int)httpParams.Page-1) * take;
        }
    }
}
