using MainTz.Application.Models.CarEntities;
using MainTz.Web.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public ManagerController(ICarService carService, IMapper mapper)
        {
            _mapper = mapper;
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var carsDomainEntity = await _carService.GetCarsAsync();
            var carsResponse = _mapper.Map<List<CarResponse>>(carsDomainEntity);
            return View(carsResponse);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<Car>(carRequest);
            var result = await _carService.CreateCarAsync(carDomainEntity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<Car>(carRequest);
            var result = await _carService.DeleteCarAsync(carDomainEntity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<Car>(carRequest);
            var result = await _carService.UpdateCarAsync(carDomainEntity);
            return RedirectToAction("Index");
        }
    }
}
