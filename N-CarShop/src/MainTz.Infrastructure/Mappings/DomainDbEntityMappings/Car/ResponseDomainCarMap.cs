using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    internal class ResponseDomainCarMap : Profile
    {
        public ResponseDomainCarMap() 
        {
            CreateMap<CarDomainEntity, CarRequest>();
        }
    }
}
