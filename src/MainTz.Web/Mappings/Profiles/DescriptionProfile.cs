using MainTz.Web.ViewModels.DescriptionViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class DescriptionProfile : Profile
    {
        public DescriptionProfile()
        {
            CreateMap<Description, DescriptionResponse>();
            CreateMap<DescriptionRequest, DescriptionRequest>();
        }
    }
}