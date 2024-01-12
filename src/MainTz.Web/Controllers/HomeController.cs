using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }
        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}