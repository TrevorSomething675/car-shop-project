using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainDbEntityCarMap : Profile
    {
        public DomainDbEntityCarMap()
        {
            CreateMap<Application.Models.CarEntities.Car, CarEntity>();
            CreateMap<CarEntity, Application.Models.CarEntities.Car>();
        }
    }
}