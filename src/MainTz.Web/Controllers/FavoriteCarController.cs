using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MainTz.Core.Enums;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class FavoriteCarController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFavoriteCarService _favoriteCarService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public FavoriteCarController(ICarService carService, IMapper mapper,
            IFavoriteCarService favoriteCarService, IHttpContextAccessor contextAccessor)
        {
            _favoriteCarService = favoriteCarService;
            _contextAccessor = contextAccessor;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IResult> ChangeFavoriteCar([FromBody]int id)
        {
            var result = await _favoriteCarService.ChangeFavoriteCarAsync(id);
            if (result)
                return Results.Ok();
            else
                return Results.BadRequest();
        }
        public async Task<IActionResult> GetFavoriteBigCarCard(int id)
        {
            var carModel = await _carService.GetCarByIdAsync(id);
            var carResponse = _mapper.Map<CarResponse>(carModel);

            return View(carResponse);
        }
        public async Task<IActionResult> GetFavoriteCars(int pageNumber = 1)
        {
            var userId = Convert.ToInt32(_contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
            var carsModel = await _carService.GetCarsAsync(userId, pageNumber, carType: CarType.Favorite);
            var totalCars = (await _carService.GetCarsAsync(userId, null, carType: CarType.Favorite)).Count() / 8f;
            var carsResponse = _mapper.Map<List<CarResponse>>(carsModel);
            var favoriteCarsModel = new CarsViewModel
            {
                PageCount = (int)Math.Ceiling(totalCars),
                PageNumber = pageNumber,
                CarsResponse = carsResponse,
            };

            return View(favoriteCarsModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetFavoriteCarsPartial([FromBody] int pageNumber = 1)
        {
            var id = Convert.ToInt32(_contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
            var carsDomainModels = await _carService.GetCarsAsync(id, pageNumber, carType: CarType.Favorite);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);

            return PartialView(carsResponse);
        }
    }
}