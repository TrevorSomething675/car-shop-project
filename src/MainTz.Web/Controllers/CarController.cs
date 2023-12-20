using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MainTz.Application.Models.CarEntities;

namespace MainTz.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public CarController(ICarService carService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetCars(int pageNumber = 1, CarsViewModel customCarsModel = null)
        {
            if(customCarsModel.CarsResponse == null)
            {
                var carsDomainModels = await _carService.GetCarsAsync(pageNumber);
                var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);

                var model = new CarsViewModel
                {
                    PageNumber = pageNumber,
                    CarsResponse = carsResponse,
                };
                return View(model);
            }
            return View(customCarsModel);
        }
        public async Task<IActionResult> GetBigCarCard(int id)
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Response.Cookies.Append("LastOpenedCarCard", id.ToString());
                return RedirectToAction("Login", "Auth");
            }
            var carModel = await _carService.GetCarByIdAsync(id);
            var carResponse = _mapper.Map<CarResponse>(carModel);
            HttpContext.Response.Cookies.Delete("LastOpenedCarCard");

            return View(carResponse);
        }
        public async Task<IActionResult> GetFavoriteCars(int pageNumber = 1)
        {
            var carsModel = await _carService.GetFavoriteCarsAsync(pageNumber);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsModel);
            var customCarsModel = new CarsViewModel
            {
                PageNumber = pageNumber,
                CarsResponse = carsResponse
            };

            return RedirectToAction("GetCars", customCarsModel);
        }
    }
}
