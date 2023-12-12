using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User;
using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.User;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.Car;

namespace MainTz.Web.Mappings
{
	static public class AutoMapperConfiguration
	{
		static public IServiceCollection AddDomainAppAutoMapperConfiguration(this IServiceCollection services)
		{
			services.AddAutoMapper(config =>
			{
				config.AddProfile<ResponseCarProfile>();
				config.AddProfile<DomainEntityCarProfile>();
				config.AddProfile<RequestCarProfile>();

				config.AddProfile<RequestDomainUserMap>();
				config.AddProfile<DomainEntityUserProfile>();
				config.AddProfile<ResponseDomainUserMap>();
			});

			return services;
		}
	}
}