using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.ViewModels;

namespace TestIdentity.Identity.ViewComponents
{
    public class DropDownAction : ViewComponent
    {
        public IViewComponentResult Invoke(string controller, dynamic routedata, string returnurl="")
        {
            DropDownActionModel model = new DropDownActionModel();
            List<DropDownActionItemModel> items = new List<DropDownActionItemModel>();


            var routeValueDictionary = new RouteValueDictionary(routedata);
            Dictionary<string, string> allRoute = new Dictionary<string, string>();

            foreach(var key in routeValueDictionary.Keys)
            {
                allRoute.Add(key, routeValueDictionary[key].ToString());
            }

            items.Add(new DropDownActionItemModel() { Controller = controller, Action = "Details", Header = "View", RouteData = allRoute, ReturnUrl = returnurl });
            items.Add(new DropDownActionItemModel() { Controller = controller, Action = "EditStart", Header = "Edit", RouteData = allRoute, ReturnUrl = returnurl });
            items.Add(new DropDownActionItemModel() { Controller = controller, Action = "DeleteStart", Header = "Delete", RouteData = allRoute, ReturnUrl = returnurl });

            if(string.Compare(controller,"clients",true)==0)
            {
                items.Add(new DropDownActionItemModel() { Controller = "clientscopes", Action = "Index", Header = "Scopes", RouteData = allRoute });
                items.Add(new DropDownActionItemModel() { Controller = "clientgranttypes", Action = "Index", Header = "Grant Types", RouteData = allRoute });
            }

            model.Items = items;

            return View("_DropDownAction", model);
        }
    }
}
