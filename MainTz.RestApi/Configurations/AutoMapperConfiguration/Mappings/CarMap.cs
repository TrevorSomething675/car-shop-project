using MainTz.RestApi.dal.Data.Models.DtoModels;
using MainTz.RestApi.dal.Data.Models.Entities;
using AutoMapper;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings
{
    public class CarMap : Profile
    {
        public CarMap() 
        {
            CreateMap<Car, CarDto>().ReverseMap();
        }
    }
}
