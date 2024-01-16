using MainTz.Application.Models.CarEntities;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<List<Car>> GetCarsAsync();
        public Task<List<Car>> GetCarsWithPaggingAsync(int pageNumber = 1);
        public Task<List<Car>> GetFavoriteCarsAsync(int pageNumber = 1);
        public Task<List<Car>> GetFavoriteCarsWithPaggingAsync(int pageNumber = 1);

        public Task<bool> CreateCarAsync(Car car);
        public Task<bool> DeleteCarAsync(Car car);
        public Task<bool> UpdateCarAsync(Car car);
    }
}