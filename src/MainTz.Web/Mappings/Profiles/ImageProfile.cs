using MainTz.Web.ViewModels.ImageViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageRequest, Image>();
            CreateMap<Image, ImageResponse>();
        }
    }
}