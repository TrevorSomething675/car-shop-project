using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    public class MessageController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ErrorViewPartial(string error)
        {
            return PartialView("ErrorViewPartial", error);
        }
        [HttpGet]
        public async Task<IActionResult> InfoViewPartial(string message)
        {
            return PartialView("InfoViewPartial", message);
        }
    }
}