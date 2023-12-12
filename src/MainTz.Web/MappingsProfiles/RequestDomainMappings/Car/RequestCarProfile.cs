using MainTz.Web.ViewModels.CarViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.Car
{
    internal class RequestCarProfile : Profile
    {
        public RequestCarProfile()
        {
            CreateMap<Application.Models.CarEntities.Car, CarRequest>();
        }
    }
}