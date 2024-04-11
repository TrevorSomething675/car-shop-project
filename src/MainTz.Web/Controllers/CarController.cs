using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using MainTz.Application.Models;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBrandService _brandService;
        private readonly IUserService _userService;
        private readonly ICarService _carService;
		private readonly IMapper _mapper;
        public CarController(ICarService carService, IMapper mapper, IHttpContextAccessor contextAccessor,
            IBrandService brandService, IUserService userService, INotificationService notificationService)
        {
            _notificationService = notificationService;
            _contextAccessor = contextAccessor;
            _brandService = brandService;
            _userService = userService;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetCars(int pageNumber = 1, CarsViewModel customCarsModel = null)
        {
            if(customCarsModel.CarsResponse == null)
            {
                if (_contextAccessor.HttpContext.User.Identity.Name != null)
                {
                    var user = await _userService.GetUserByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                    if(user.Role.Name == "Manager" || user.Role.Name == "Admin")
                    {
						var carsModelWithHidden = await _carService.GetCarsWithPaggingWithHiddenAsync(pageNumber);
						var carsResponseWithHidden = _mapper.Map<List<CarResponse>>(carsModelWithHidden);
						var totalCarsWithHidden = (await _carService.GetCarsWithHiddenAsync()).Count() / 8f;
						var modelCarsWithHidden = new CarsViewModel
						{
							PageCount = (int)Math.Ceiling(totalCarsWithHidden),
							PageNumber = pageNumber,
							CarsResponse = carsResponseWithHidden,
						};
						return View(modelCarsWithHidden);
					}
                }
				var carsModel = await _carService.GetCarsWithPaggingAsync(pageNumber);
                var carsResponse = _mapper.Map<List<CarResponse>>(carsModel);
                var totalCars = (await _carService.GetCarsAsync()).Count() / 8f;
                var model = new CarsViewModel
                {
                    PageCount = (int)Math.Ceiling(totalCars),
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
            if (carResponse == null)
                return Redirect("/Error");
            else
                return View(carResponse);
        }
        [HttpPost]
        public async Task<IActionResult> GetCarsPartial([FromBody]int pageNumber = 1)
        {
           var carsDomainModels = await _carService.GetCarsWithPaggingAsync(pageNumber);
           var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);

            return PartialView(carsResponse);
        }
        public async Task<IActionResult> GetCreateCar()
        {
            var brandsWithModels = await _brandService.GetBrandsAsync();
            var brandsWithModelsResponse = _mapper.Map<List<BrandResponse>>(brandsWithModels);

            var model = new CreateCarResponse()
            {
                BrandsResponse = brandsWithModelsResponse,
            };

            return View(model);
        }
        public async Task<IResult> CreateCarCommand(CarRequest carRequest)
        {
            try
            {
                var car = _mapper.Map<Car>(carRequest);
                var addedCar = await _carService.CreateCarAsync(car);
                return Results.Ok(addedCar.Id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = ex.Message});
            }
        }
        public async Task<IActionResult> ChangeCarVisible(int id)
        {
            var car = await _carService.ChangeCarVisible(id);
            if (car.IsVisible)
            {
                var newNotification = new Notification
                {
                    Header = "Товар доступен в магазине",
                    Description = $"Теперь машина {car.Name} снова доступна в магазине!"
                };
                var notificationHasBeenSended = await _notificationService.SendNotificationOnCarIdWithDescription(id, newNotification);
            }
            var addedCar = await _carService.GetCarByIdAsync(car.Id);
            var carResponse = _mapper.Map<CarResponse>(addedCar);
            return RedirectToAction("GetBigCarCard", new {id = carResponse.Id });
        }
        public async Task<IActionResult> GetUpdateCar(int id)
        {
			var brands = await _brandService.GetBrandsAsync();
			var brandsResponse = _mapper.Map<List<BrandResponse>>(brands);
            var car = await _carService.GetCarByIdAsync(id);
            var carResponse = _mapper.Map<CarResponse>(car);

            var model = new UpdateCarResponse()
            {
                Car = carResponse,
                BrandsResponse = brandsResponse
            };
			return View(model);
        }
		public async Task<IResult> UpdateCarCommand(CarRequest carRequest)
        {
            var car = _mapper.Map<Car>(carRequest);
            var updatedCar = await _carService.UpdateCarAsync(car);
            var carResponse = _mapper.Map<CarResponse>(updatedCar);

            return Results.Json(carResponse.Id);
		}
        public async Task<IResult> RemoveCarById([FromBody]int id)
        {
            var newNotification = new Notification()
            {
                Header = $"Машина была удалена {id}",
                Description = $"Машина была снята с продажи {id}"
            };
            var notificationHasBeenSended = await _notificationService.SendNotificationOnCarIdWithDescription(id, newNotification);
            var carHasBeenDeleted = await _carService.RemoveCarByIdAsync(id);

            if (carHasBeenDeleted && notificationHasBeenSended)
                return Results.Ok();
            else
                return Results.BadRequest();
        }
	}
}