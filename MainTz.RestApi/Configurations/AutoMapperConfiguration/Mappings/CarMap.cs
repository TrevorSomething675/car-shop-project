using MainTz.RestApi.Data.Models.DtoModels;
using MainTz.RestApi.Data.Models.Entities;
using AutoMapper;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings
{
    public class CarMap : Profile
    {
        public CarMap() 
        {
            CreateMap<Car, CarDto>();

            CreateMap<CarDto, Car>().ForMember(car => car.Id, opt => opt.Ignore());
        }
    }
}
