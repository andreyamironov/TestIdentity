using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Controllers
{
    public class RedirectController : Controller
    {
        [HttpPost]
        public IActionResult Index(string url)
        {
            return Redirect(url);
        }
    }
}
