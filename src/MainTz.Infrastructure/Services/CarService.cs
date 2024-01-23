using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Http;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMinioService _minioService;
        private readonly IMapper _mapper;
        public CarService(IUserRepository userRepository, ICarRepository carRepository, 
            IMapper mapper, ILogger<CarService> logger, IHttpContextAccessor contextAccessor,
            IMinioService minioService)
        {
            _logger = logger;
            _mapper = mapper;
            _minioService = minioService;
            _carRepository = carRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
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
        public async Task<List<Car>> GetCarsAsync()
        {
            var carsEntity = await _carRepository.GetCarsAsync();
            var carsDomainEntity = _mapper.Map<List<Car>>(carsEntity);
            return carsDomainEntity;
        }
        public async Task<List<Car>> GetFavoriteCarsWithPaggingAsync(int pageNumber = 1)
        {
            var totalCarsInPage = 8f;
            var user = await _userRepository.GetUserByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var pageCount = Math.Ceiling(user.Cars
                .Count() / totalCarsInPage);

            var carsEntity = user.Cars
                .Take((int)(totalCarsInPage * pageNumber))
                .Skip((int)(totalCarsInPage * (pageNumber - 1)))
                .ToList();

            var carsDomainEntity = _mapper.Map<List<Car>>(carsEntity);
            foreach (var car in carsDomainEntity)
            {
                foreach (var image in car.Images)
                {
                    image.FileBase64String = await _minioService.GetObjectAsync(image.Path);
                }
            }
            return carsDomainEntity;
        }
        public async Task<List<Car>> GetFavoriteCarsAsync(int pageNumber = 1)
        {
            var user = await _userRepository.GetUserByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var carsEntity = user.Cars.ToList();
            var carsDomainEntity = _mapper.Map<List<Car>>(carsEntity);

            return carsDomainEntity;
        }
        public async Task<List<Car>> GetCarsWithPaggingAsync(int pageNumber = 1)
        {
            var totalCarsInPage = 8f;
            var pageCount = Math.Ceiling(_carRepository.GetCarsAsync().Result.Count() / totalCarsInPage);

            var carsEntity = _carRepository.GetCarsAsync().Result
                .Take((int)(totalCarsInPage * pageNumber))
                .Skip((int)(totalCarsInPage * (pageNumber - 1)))
                .ToList();

            var carsDomainEntity = _mapper.Map<List<Car>>(carsEntity);
            foreach (var car in carsDomainEntity)
            {
                foreach (var image in car.Images)
                {
                    image.FileBase64String = await _minioService.GetObjectAsync(image.Path);
                }
            }
            return carsDomainEntity;
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
        public async Task<bool> DeleteCarAsync(Car car)
        {
            try
            {
                await _carRepository.DeleteAsync(car);
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