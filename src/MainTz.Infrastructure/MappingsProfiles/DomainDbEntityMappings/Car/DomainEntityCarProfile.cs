using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class DomainEntityCarProfile : Profile
    {
        public DomainEntityCarProfile()
        {
            CreateMap<Application.Models.CarEntities.Car, CarEntity>()
                .ForMember(dest => dest.Images, opts => opts.Ignore())

                .ForMember(dest => dest.Users, opts => opts.Ignore())
                .ForMember(dest => dest.UserId, opts => opts
                    .MapFrom(src => src.UsersId))

                .ForMember(dest => dest.Model, opts => opts.Ignore())
                .ForMember(dest => dest.ModelId, opts => opts
                    .MapFrom(src => src.ModelId)).ReverseMap();
        }
    }
}