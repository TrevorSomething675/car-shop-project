using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
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
