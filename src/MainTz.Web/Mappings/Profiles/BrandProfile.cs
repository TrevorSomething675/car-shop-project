using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Application.Models;
using AutoMapper;

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
