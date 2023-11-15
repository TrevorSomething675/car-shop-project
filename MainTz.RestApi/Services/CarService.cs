using MainTz.RestApi.Repositories.Abstractions;
using MainTz.RestApi.Data.Models.DtoModels;
using MainTz.RestApi.Services.Abstractions;
using MainTz.RestApi.Data.Models.Entities;
using System.Linq.Expressions;
using AutoMapper;

namespace MainTz.RestApi.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, ILogger<CarService> logger)
        {
            _logger = logger;
            _mapper  = mapper;
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            var cars = await _carRepository.GetCars(filter);
            var carsDto = _mapper.Map<List<CarDto>>(cars);

            return carsDto;
        }

        public async Task CreateCar(CarDto carDto)
        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                await _carRepository.Create(car);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
			}
        }

        public async Task DeleteCar(CarDto carDto)
        {
            try
            {
                var resultCar = _carRepository.GetCar(car => 
                    car.Model == carDto.Model &&
                    car.Brand == carDto.Brand &&
                    car.Color == carDto.Color).Result;

                if(resultCar != null)
                    await _carRepository.Delete(resultCar);
            }
            catch(Exception ex)
            {
				_logger.LogInformation($"{ex.Message}");
			}
        }

        public Task UpdateCar(CarDto carDto)
        {
            throw new NotImplementedException();
        }
    }
}
