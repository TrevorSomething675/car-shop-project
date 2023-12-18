using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public UserController(ICarService carService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var carsDomainModels = await _carService.GetCarsAsync(pageNumber);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);

            var model = new CarsViewModel
            {
                pageNumber = pageNumber,
                CarsResponse = carsResponse,
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CarBigCard(int id)
        {
            if(!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Response.Cookies.Append("LastOpenedCarCard", id.ToString());
                return RedirectToAction("Login", "Auth");
            }
            var carModel = await _carService.GetCarByIdAsync(id);
            var carsResponse = _mapper.Map<CarResponse>(carModel);
            HttpContext.Response.Cookies.Delete("LastOpenedCarCard");

            return View(carsResponse);
        }
    }
}