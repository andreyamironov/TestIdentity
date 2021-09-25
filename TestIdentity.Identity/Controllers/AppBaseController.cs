using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Controllers
{
    public class AppBaseController : Controller
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IDiagnosticContext _diagnosticContext;

        public AppBaseController(IWebHostEnvironment webHostEnvironment ,IDiagnosticContext diagnosticContext)
        {
            _webHostEnvironment = webHostEnvironment;

            _diagnosticContext = diagnosticContext ??
                throw new ArgumentNullException(nameof(diagnosticContext));
        }
    }
}
