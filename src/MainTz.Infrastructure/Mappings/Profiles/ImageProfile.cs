using MainTz.Application.Models.CarModels;
using MainTz.Database.Entities;
using AutoMapper;

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