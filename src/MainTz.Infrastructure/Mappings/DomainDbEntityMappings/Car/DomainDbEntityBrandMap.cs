using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainDbEntityBrandMap : Profile
    {
        public DomainDbEntityBrandMap() 
        {
            CreateMap<Brand, BrandEntity>();
            CreateMap<BrandEntity, Brand>();
        }
    }
}
