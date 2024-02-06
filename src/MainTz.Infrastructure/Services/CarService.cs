using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Application.Models;
using MainTz.Core.Enums;
using AutoMapper;

namespace MainTz.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMinioService _minioService;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper,
            ILogger<CarService> logger, IMinioService minioService)
        {
            _logger = logger;
            _mapper = mapper;
            _minioService = minioService;
            _carRepository = carRepository;
        }
        public async Task<Car> GetCarByIdAsync(int id)
        {
            var carEntity = await _carRepository.GetCarByIdAsync(id);
            var car = _mapper.Map<Car>(carEntity);
            foreach (var image in car.Images)
            {
                image.FileBase64String = await _minioService.GetObjectAsync(image.Path);
            }
            return car;
        }
        public async Task<CarsModel> GetCarsAsync(int userId, int? pageNumber = null, CarType carType = CarType.Default)
        {
            var cars = new List<Car>();
            var carsModel = new CarsModel();
            switch (carType)
            {
                case CarType.Default:
                    cars = await _carRepository.GetCarsAsync(userId, pageNumber);
                    var totalCars = (await _carRepository.GetCarsAsync(userId, null)).Count() / 8f;
                    carsModel.Cars = cars;
                    carsModel.PageNumber = pageNumber;
                    carsModel.PageCount = (int)Math.Ceiling(totalCars);
                    break;
                case CarType.Favorite:
                    cars = await _carRepository.GetFavoriteCarsAsync(userId, pageNumber);
                    var totalFavoriteCars = (await _carRepository.GetFavoriteCarsAsync(userId, null)).Count() / 8f;
                    carsModel.Cars = cars;
                    carsModel.PageNumber = pageNumber;
                    carsModel.PageNumber = (int)Math.Ceiling(totalFavoriteCars);
                    break;
            }
            foreach (var car in cars)
            {
                foreach (var image in car.Images)
                {
                    image.FileBase64String = await _minioService.GetObjectAsync(image.Path);
                }
            }

            return carsModel;
        }
        public async Task<Car> CreateCarAsync(Car car)
        {
            try
            {
                var checkDbCar = await _carRepository.GetCarByNameAsync(car.Name);
                if (checkDbCar != null)
                    throw new Exception("Машина уже существует в базе данных");

				foreach (var image in car.Images)
                {
                    var path = await _minioService.CreateObjectAsync(image);
                    image.Path = path;
                }
                var addedCar = await _carRepository.CreateAsync(car);
                return addedCar;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> RemoveCarByIdAsync(int id)
        {
            try
            {
                await _carRepository.RemoveCarByIdAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<Car> UpdateCarAsync(Car car)
        {
            try
            {
				foreach (var image in car.Images)
				{
					var path = await _minioService.CreateObjectAsync(image);
					image.Path = path;
				}
                var updatedCar = await _carRepository.UpdateAsync(car);
				return updatedCar;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception("Не удалось обновить машину");
            }
        }
        public async Task<Car> ChangeCarVisible(int id)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if(car.IsVisible)
                    car.IsVisible = false;
                else
                    car.IsVisible = true;

                var addedCar = await _carRepository.UpdateAsync(car);
                return addedCar;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}