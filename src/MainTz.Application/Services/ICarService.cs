using MainTz.Application.Models.CarEntities;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<List<Car>> GetCars();
        public Task<bool> CreateCar(Car carDto);
        public Task<bool> DeleteCar(Car carDto);
        public Task<bool> UpdateCar(Car carDto);
    }
}