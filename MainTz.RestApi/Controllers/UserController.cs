using MainTz.RestApi.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
	public class UserController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<UserController> _logger;
        public UserController(ICarService carService, ILogger<UserController> logger) 
        {
            _logger = logger;
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _carService.GetCars();
			return View(model);
        }

        [HttpGet("GetJija")]
        [Authorize]
        public async Task<IActionResult> GetInfo()
        {
            return Ok("Jija");
        }
    }
}
