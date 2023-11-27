using MainTz.RestApi.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
	public class UserController : Controller
    {
        private readonly ICarService _carService;
        public UserController(ICarService carService) 
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _carService.GetCars();
			return View(model);
        }
    }
}
