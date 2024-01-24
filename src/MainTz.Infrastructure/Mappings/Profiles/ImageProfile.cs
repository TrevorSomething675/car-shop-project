using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageEntity>().ReverseMap();
        }
    }
}