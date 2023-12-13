using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainDbEntityCarMap : Profile
    {
        public DomainDbEntityCarMap()
        {
            CreateMap<CarDomainEntity, CarEntity>();

            CreateMap<CarEntity, CarDomainEntity>();
        }
    }
}