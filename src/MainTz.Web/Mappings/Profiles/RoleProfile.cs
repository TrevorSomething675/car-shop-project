using MainTz.Web.ViewModels.RoleViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<RoleRequest, Role>();
            CreateMap<Role, RoleResponse>();
        }
    }
}