using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainDbEntityModelMap : Profile
    {
        public DomainDbEntityModelMap()
        {
            CreateMap<Model, ModelEntity>();
            CreateMap<ModelEntity, Model>();
        }
    }
}
