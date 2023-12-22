using MainTz.Application.Models.CarEntities;
using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, ILogger<CarService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _carRepository = carRepository;
        }
        public async Task<Car> GetCarByIdAsync(int id)
        {
            var carEntity = await _carRepository.GetCarByIdAsync(id);
            var car = _mapper.Map<Car>(carEntity);

            return car;
        }
        public async Task<List<Car>> GetCarsAsync()
        {
            var carsEntity = await _carRepository.GetCarsAsync();
            var carsDomainEntity = _mapper.Map<List<Car>>(carsEntity);
            return carsDomainEntity;
        }
        public async Task<List<Car>> GetFavoriteCarsAsync(int pageNumber = 1)
        {
            var totalCarsInPage = 8f;
            var pageCount = Math.Ceiling(_carRepository.GetCarsAsync(car => car.IsFavorite == true).Result.Count() / totalCarsInPage);

            var carsEntity = _carRepository.GetCarsAsync(car => car.IsFavorite == true).Result
                .Take((int)(totalCarsInPage * pageNumber))
                .Skip((int)(totalCarsInPage * (pageNumber - 1)))
                .ToList();

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

            return carsDomainEntity;
        }
        public async Task<bool> CreateCarAsync(Car carDomainEntity)
        {
            try
            {
                var carEntity = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.CreateAsync(carEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteCarAsync(Car carDomainEntity)
        {
            try
            {
                var carEntity = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.DeleteAsync(carEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateCarAsync(Car carDomainEntity)
        {
            try
            {
                var car = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.UpdateAsync(car);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
    }
}
