using MainTz.RestApi.dal.Data.Models.DtoModels;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
    public interface ICarService
    {
        public Task<List<CarDto>> GetCars();
        public Task<bool> CreateCar(CarDto carDto);
        public Task<bool> DeleteCar(CarDto carDto);
        public Task<bool> UpdateCar(CarDto carDto);
    }
}
