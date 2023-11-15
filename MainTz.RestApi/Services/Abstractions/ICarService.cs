using MainTz.RestApi.Data.Models.DtoModels;
using MainTz.RestApi.Data.Models.Entities;
using System.Linq.Expressions;

namespace MainTz.RestApi.Services.Abstractions
{
    public interface ICarService
    {
        public Task<List<CarDto>> GetCars(Expression<Func<Car, bool>> filter = null);
        public Task CreateCar(CarDto carDto);
        public Task UpdateCar(CarDto carDto);
        public Task DeleteCar(CarDto carDto);
    }
}
