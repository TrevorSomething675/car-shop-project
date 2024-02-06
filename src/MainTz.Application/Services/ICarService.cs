using MainTz.Application.Models;
using MainTz.Core.Enums;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<CarsModel> GetCarsAsync(int userId, int? pageNumber = null, CarType carType = CarType.Default);

        public Task<Car> ChangeCarVisible(int id);
        public Task<Car> CreateCarAsync(Car car);
        public Task<bool> RemoveCarByIdAsync(int id);
        public Task<Car> UpdateCarAsync(Car car);
    }
}