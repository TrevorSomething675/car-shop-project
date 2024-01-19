using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models.CarModels;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageRequest, Image>().ReverseMap();
            CreateMap<ImageResponse, Image>().ReverseMap();
        }
    }
}