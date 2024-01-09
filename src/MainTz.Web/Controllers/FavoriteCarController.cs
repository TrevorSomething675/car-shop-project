using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class FavoriteCarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public FavoriteCarController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetFavoriteCars(int pageNumber = 1)
        {
            var carsModel = await _carService.GetFavoriteCarsAsync(pageNumber);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsModel);
            var favoriteCarsModel = new CarsViewModel
            {
                PageNumber = pageNumber,
                CarsResponse = carsResponse
            };

            return View(favoriteCarsModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetFavoriteCarsPartial([FromBody] int pageNumber = 1)
        {
            var carsDomainModels = await _carService.GetCarsWithPaggingAsync(pageNumber);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);

            return PartialView(carsResponse);
        }
    }
}