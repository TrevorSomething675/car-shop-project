using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class CarsModelProfile : Profile
    {
        public CarsModelProfile() 
        {
            CreateMap<CarsModel, CarsModelResponse>();
        }
    }
}