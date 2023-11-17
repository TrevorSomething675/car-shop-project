using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.DtoModels;
using MainTz.RestApi.dal.Data.Models.Entities;
using AutoMapper;

namespace MainTz.RestApi.BLL.Services
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

        public async Task<List<CarDto>> GetCars()
        {
            var cars = await _carRepository.GetCars();
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
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }

        public async Task DeleteCar(CarDto carDto)
        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                await _carRepository.Delete(car);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }
    }
}
