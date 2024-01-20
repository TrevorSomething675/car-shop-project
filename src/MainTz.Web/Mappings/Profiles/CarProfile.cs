using MainTz.Web.ViewModels.CarViewModels;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Web.Mappings.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarRequest, Car>();
            CreateMap<Car, CarResponse>();
        }
    }
}