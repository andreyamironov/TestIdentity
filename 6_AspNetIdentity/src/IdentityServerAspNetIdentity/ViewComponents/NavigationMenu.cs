using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;

namespace IdentityServerAspNetIdentity.ViewComponents
{
    public class NavigationMenu:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<MenuItem> model = new List<MenuItem>();

            if (User.Identity.IsAuthenticated && User.IsInRole(Enum.GetName(typeof(Core.RoleRight), Core.RoleRight.Administrator)))
            {
                model.Add(new MenuItem() { Controller = "Users", Action = "Index", Header = "Users" });
                model.Add(new MenuItem() { Controller = "Clients", Action = "Index", Header = "Clients" });
                model.Add(new MenuItem() { Controller = "IdentityResouces", Action = "Index", Header = "IdentityResouces" });
                model.Add(new MenuItem() { Controller = "ApiScopes", Action = "Index", Header = "ApiScopes" });
                model.Add(new MenuItem() { Controller = "LogEvents", Action = "Index", Header = "Journal" }); 
            }

            return View("_NavigationMenu", model);
        }
    }
}
