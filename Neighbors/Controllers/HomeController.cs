using Microsoft.AspNetCore.Mvc;

namespace Neighbors.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}