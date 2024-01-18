using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models.CarModels;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class RequestDomainCarMap : Profile
    {
        public RequestDomainCarMap()
        {
            CreateMap<CarRequest, Car>().ReverseMap();
            CreateMap<CarResponse, Car>().ReverseMap();
        }
    }
}