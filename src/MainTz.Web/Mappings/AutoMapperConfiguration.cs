using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User;
using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.User;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.Car;
using MainTz.Web.Mappings.RequestDomainMappings.User;

namespace MainTz.Web.Mappings
{
	static public class AutoMapperConfiguration
	{
		static public IServiceCollection AddDomainAppAutoMapperConfiguration(this IServiceCollection services)
		{
			services.AddAutoMapper(config =>
			{
				config.AddProfile<RequestDomainCarMap>();
				config.AddProfile<DomainDbEntityCarMap>();
				config.AddProfile<ResponseDomainCarMap>();

				config.AddProfile<RequestDomainUserMap>();
				config.AddProfile<DomainDbEntityUserMap>();
				config.AddProfile<DomainDbEntityRoleMap>();
				config.AddProfile<DomainDbEntityNotificationMap>();
				config.AddProfile<ResponseDomainUserMap>();
				config.AddProfile<ResponseDomainNotificationMap>();
			});

			return services;
		}
	}
}
