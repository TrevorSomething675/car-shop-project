using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        //private readonly IMinioService _minioService;
        public CarController(/*IMinioService minioService,*/ ICarService carService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            //_minioService = minioService;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetCars(int pageNumber = 1, CarsViewModel customCarsModel = null)
        {
            //var jija = await _minioService.GetObjectByNameAndBucket("test-bucket-1", "Kia-Rio-image-1.jpg");
            if(customCarsModel.CarsResponse == null)
            {
                var carsDomainModels = await _carService.GetCarsWithPaggingAsync(pageNumber);
                var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainModels);
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
    }
}
