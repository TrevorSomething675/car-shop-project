using MainTz.RestApi.dal.Data.Models.DtoModels;
using AutoMapper;
using MainTz.RestApi.DAL.Data.Models.Entities;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings
{
    /// <summary>
    /// Конфигурация маппинга для Car и CarDto
    /// </summary>
    public class CarMap : Profile
    {
        public CarMap() 
        {
            CreateMap<Car, CarDto>().ReverseMap();
        }
    }
}
