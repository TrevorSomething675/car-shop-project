using MainTz.Web.ViewModels.CarModelViewModel;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Web.Mappings.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile() 
        {
            CreateMap<ModelRequest, Model>();
            CreateMap<Model, ModelResponse>();
        }
    }
}
