using MainTz.Web.ViewModels.CarModelViewModel;
using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using MainTz.Application.Models;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using AutoMapper;
using MainTz.Web.ViewModels.ImageViewModels;

namespace MainTz.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly ICarService _carService;
        private readonly IImageService _imageService;
		private readonly IMapper _mapper;
        public CarController(ICarService carService, IMapper mapper, IHttpContextAccessor contextAccessor,
            IBrandService brandService, IModelService modelService, INotificationService notificationService
            ,IImageService imageService)
        {
            _notificationService = notificationService;
            _contextAccessor = contextAccessor;
            _imageService = imageService;
            _brandService = brandService;
            _modelService = modelService;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IResult> GetCars(int pageNumber = 1)
        {
            var id = Convert.ToInt32(_contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);

            var carsModel = await _carService.GetCarsAsync(id, pageNumber);
			var carsResponse = _mapper.Map<List<CarResponse>>(carsModel);
			var totalCarsWithHidden = (await _carService.GetCarsAsync(id, null)).Count() / 8f;
			var modelCarsWithHidden = new CarsViewModel
			{
				PageCount = (int)Math.Ceiling(totalCarsWithHidden),
				PageNumber = pageNumber,
				CarsResponse = carsResponse,
			};
            return Results.Ok(modelCarsWithHidden);
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
            var image = await _imageService.GetImageByIdAsync(carResponse.Id);
            var imageResponse = _mapper.Map<ImageResponse>(image);

            HttpContext.Response.Cookies.Delete("LastOpenedCarCard");
            var CarCardViewMode = new CarCardViewModel()
            {
                CarResponse = carResponse,
                ImageResponse = imageResponse,
            };

            return View(CarCardViewMode);
        }
        [HttpPost]
        public async Task<IActionResult> GetCarsPartial([FromBody]int pageNumber = 1)
        {
            var id = Convert.ToInt32(_contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);

            var carsDomainModels = await _carService.GetCarsAsync(id, pageNumber);
            var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);
            var model = new CarsViewModel
            {
                CarsResponse = carsResponse,
            };
            return PartialView(carsResponse);
        }
        public async Task<IActionResult> GetCreateCar()
        {
            var brandsWithModels = await _brandService.GetBrandsWithModelsAsync();
            var brandsWithModelsResponse = _mapper.Map<List<BrandResponse>>(brandsWithModels);
            var models = await _modelService.GetModels();
            var modelsResponse = _mapper.Map<List<ModelResponse>>(models);

            var model = new CreateCarResponse()
            {
                BrandsResponse = brandsWithModelsResponse,
                ModelsResponse = modelsResponse
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
			var brandsWithModels = await _brandService.GetBrandsWithModelsAsync();
			var brandsWithModelsResponse = _mapper.Map<List<BrandResponse>>(brandsWithModels);
			var models = await _modelService.GetModels();
			var modelsResponse = _mapper.Map<List<ModelResponse>>(models);
            var car = await _carService.GetCarByIdAsync(id);
            var carResponse = _mapper.Map<CarResponse>(car);
            var image = await _imageService.GetImageByIdAsync(carResponse.Id);
            var imageResponse = _mapper.Map<ImageResponse>(image);

            var model = new UpdateCarResponse()
            {
                Car = carResponse,
                Image = imageResponse,
                ModelsResponse = modelsResponse,
                BrandsResponse = brandsWithModelsResponse
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