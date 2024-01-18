using MainTz.Application.Models.CarEntities;
using MainTz.Web.ViewModels.CarViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.RequestDomainMappings.Car
{
    public class RequestDomainImageMap : Profile
    {
        public RequestDomainImageMap()
        {
            CreateMap<ImageRequest, Image>();
            CreateMap<Image, ImageRequest>();
        }
    }
}
