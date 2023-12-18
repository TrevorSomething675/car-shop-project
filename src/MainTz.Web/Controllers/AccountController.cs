using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}