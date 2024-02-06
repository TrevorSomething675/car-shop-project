using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.Commands;
using MainTz.Core.Enums;
using AutoMapper;
using MediatR;

namespace MainTz.Web.Controllers
{
    public class FavoriteCarController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public FavoriteCarController(ICarService carService, IMapper mapper,
            IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _carService = carService;
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPut]
        public async Task<IResult> ChangeFavoriteCar(AddCarToFavoriteCommand addCarToFavoriteCommand)
        {
            var result = await _mediator.Send(addCarToFavoriteCommand);
            if (result != null)
                return Results.Ok(result);
            else
                return Results.NotFound(result);
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
            var carsModelResponse = _mapper.Map<CarsModelResponse>(carsModel);

            return View(carsModelResponse);
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