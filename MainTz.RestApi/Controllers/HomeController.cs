using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
