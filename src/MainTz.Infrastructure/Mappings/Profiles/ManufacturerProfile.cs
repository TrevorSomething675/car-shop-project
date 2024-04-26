using MainTz.Application.Models;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.Profiles
{
	public class ManufacturerProfile : Profile
	{
		public ManufacturerProfile() 
		{
			CreateMap<Manufacturer, ManufacturerEntity>().ReverseMap();
		}
	}
}
