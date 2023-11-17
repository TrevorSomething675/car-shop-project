using MainTz.RestApi.dal.Data.Models.DtoModels;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
    public interface ICarService
    {
        public Task<List<CarDto>> GetCars();
        public Task CreateCar(CarDto carDto);
        public Task DeleteCar(CarDto carDto);
    }
}
