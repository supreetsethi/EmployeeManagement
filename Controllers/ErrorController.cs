using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _Ilogger;

        public ErrorController(ILogger<ErrorController> ILogger)
        {
            _Ilogger = ILogger;
        }
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested could not be found";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry, There is some technical issue our technical team is working on it.";
                    break;
            }
            ViewBag.Path = statusCodeResult.OriginalPath;
            ViewBag.QS = statusCodeResult.OriginalQueryString;
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _Ilogger.LogError($"The Path {exceptionDetails.Path} threw an error"
                + $"{exceptionDetails.Error.Message}");
            return View("Error");
        }
    }
}