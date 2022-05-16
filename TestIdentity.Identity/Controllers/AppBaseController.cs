using AMir.Wrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;

namespace TestIdentity.Identity.Controllers
{
    public class AppBaseController : Controller
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IDiagnosticContext _diagnosticContext;

        public string Root
        {
            get
            {
                return this.GetType().Name.Replace("Controller",string.Empty);
            }
        }
        public AppBaseController(IWebHostEnvironment webHostEnvironment ,IDiagnosticContext diagnosticContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
        }

        [HttpGet]
        public virtual IActionResult CreateStart(string returnUrl = null)
        {
            TempData.SetValue(KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW, KeyWord.KEY_TEMPDATA_ACTION_GET_ALLOW);
            return RedirectToAction("Create", new { returnUrl = returnUrl });
        }
    }
}
