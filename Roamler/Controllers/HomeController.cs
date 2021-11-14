using Microsoft.AspNetCore.Mvc;

namespace Roamler.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}