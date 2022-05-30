using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Models;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.ViewComponents
{
    public class Pager : ViewComponent
    {
        private readonly int DefaultRangeButton = 4;
        private readonly int DefaultPage = 1;
        private readonly int DefaultItemsPerPage = 10;

        public IViewComponentResult Invoke(string controller, int page, int itemsperpage, int totalCount, string search, string tag, string orderby)
        {
            if (page < 1) page = DefaultPage;
            if (itemsperpage < 1) itemsperpage = DefaultItemsPerPage;

            int tmpTotaPages = (int)Math.Round((double)totalCount / itemsperpage, MidpointRounding.ToPositiveInfinity);
            int tmpCurrentBlock = (int)Math.Round((double)page / DefaultRangeButton, MidpointRounding.ToPositiveInfinity);
            int tmpPageDown = page - 1 > 0 ? page - 1 : tmpTotaPages;
            int tmpPageUp = page + 1 <= tmpTotaPages ? page + 1 : 1;
            int tmpRangeStop = tmpCurrentBlock * DefaultRangeButton;
            int tmpRangeStart = tmpRangeStop - DefaultRangeButton + 1;
            tmpRangeStop = tmpRangeStop > tmpTotaPages ? tmpTotaPages : tmpRangeStop;

            PagerModel model = new PagerModel();
            model.Controller = controller;
            model.Page          = page;
            model.ItemsPerPage  = itemsperpage;
            model.ItemsPerPageVariant = new int[] { 10, 50, 100 ,1000};
            model.Search        = search;
            model.Tag           = tag;
            model.OrderBy       = orderby;


            model.PageDown      = tmpPageDown;
            model.PageUp        = tmpPageUp;

            model.RangeStart    = tmpRangeStart;
            model.RangeStop     = tmpRangeStop;

            return View("_Pager",model);
        }
    }
}
