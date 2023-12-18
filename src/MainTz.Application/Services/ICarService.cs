using MainTz.Application.Models.CarEntities;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<List<Car>> GetCarsAsync(int pageNumber = 1);
        public Task<bool> CreateCarAsync(Car carDto);
        public Task<bool> DeleteCarAsync(Car carDto);
        public Task<bool> UpdateCarAsync(Car carDto);
    }
}