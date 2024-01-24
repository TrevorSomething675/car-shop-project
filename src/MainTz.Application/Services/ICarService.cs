using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<List<Car>> GetCarsAsync();
        public Task<List<Car>> GetCarsWithHiddenAsync();
        public Task<List<Car>> GetCarsWithPaggingAsync(int pageNumber = 1);
		public Task<List<Car>> GetCarsWithPaggingWithHiddenAsync(int pageNumber = 1);
		public Task<List<Car>> GetFavoriteCarsAsync(int pageNumber = 1);
        public Task<List<Car>> GetFavoriteCarsWithPaggingAsync(int pageNumber = 1);

        public Task<Car> ChangeCarVisible(int id);

        public Task<Car> CreateCarAsync(Car car);
        public Task<bool> DeleteCarAsync(Car car);
        public Task<Car> UpdateCarAsync(Car car);
    }
}