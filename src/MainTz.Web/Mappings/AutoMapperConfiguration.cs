using System.Reflection;
using AutoMapper;

namespace MainTz.Web.Mappings
{
    static public class AutoMapperConfiguration
	{
		static public IServiceCollection AddAppAutoMapper(this IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(config =>
			{
				config.AddMaps(Assembly.GetAssembly(typeof(Infrastructure.AssemblyMarker)));
				config.AddMaps(Assembly.GetExecutingAssembly());
			});
			var mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);
			return services;
		}
	}
}