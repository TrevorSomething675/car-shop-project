using MainTz.Application.Models.CarEntities;
using MainTz.Web.ViewModels.CarViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.RequestDomainMappings.Car
{
    public class ResponseDomainImageMap : Profile
    {
        public ResponseDomainImageMap() 
        {
            CreateMap<ImageResponse, Image>();
            CreateMap<Image, ImageResponse>();
        }
    }
}
