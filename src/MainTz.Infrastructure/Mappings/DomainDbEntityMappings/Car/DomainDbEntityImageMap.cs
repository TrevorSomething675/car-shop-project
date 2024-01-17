using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainDbEntityImageMap : Profile
    {
        public DomainDbEntityImageMap() 
        {
            CreateMap<Image, ImageEntity>();
            CreateMap<ImageEntity, Image>();
        }
    }
}