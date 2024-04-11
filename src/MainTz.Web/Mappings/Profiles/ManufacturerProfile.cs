using MainTz.Web.ViewModels.ManufacturerViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile() 
        {
            CreateMap<ManufacturerRequest, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerResponse>();
        }
    }
}
