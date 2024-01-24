using MainTz.Web.ViewModels.CarModelViewModel;
using MainTz.Application.Models;
using AutoMapper;

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
