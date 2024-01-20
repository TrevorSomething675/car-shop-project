using MainTz.Web.ViewModels.BrandViewModels;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Web.Mappings.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile() 
        {
            CreateMap<BrandRequest, Brand>();
            CreateMap<Brand, BrandResponse>();
        }
    }
}
