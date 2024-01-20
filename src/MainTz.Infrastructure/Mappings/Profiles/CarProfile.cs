using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarEntity>().ReverseMap();
        }
    }
}
