using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandEntity>().ReverseMap();
        }
    }
}
