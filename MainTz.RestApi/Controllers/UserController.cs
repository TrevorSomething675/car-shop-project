using MainTz.RestApi.Services.Abstractions;
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
            _logger.LogInformation("Test");
            _logger.LogDebug("jija");

			return View(model);
        }
    }
}
