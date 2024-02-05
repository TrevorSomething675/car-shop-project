using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarRequest, Car>();
            CreateMap<Car, CarResponse>()
                .ForMember(car => car.ImagesId, opt => opt.MapFrom(src => src.Images.Select(img => img.Id)));
        }
    }
}