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
				config.AddProfile<DomainDbEntityCarMap>();
				config.AddProfile<ResponseDomainCarMap>();
				config.AddProfile<RequestDomainCarMap>();

				config.AddProfile<DomainDbEntityUserMap>();
				config.AddProfile<ResponseDomainUserMap>();
				config.AddProfile<RequestDomainUserMap>();
				config.AddProfile<RequestRegisterUserMap>();

				config.AddProfile<DomainDbEntityRoleMap>();

				config.AddProfile<DomainDbEntityNotificationMap>();
				config.AddProfile<ResponseDomainNotificationMap>();
				config.AddProfile<RequestDomainNotificationRequestMap>();

				config.AddProfile<DomainDbEntityModelMap>();
				config.AddProfile<DomainDbEntityBrandMap>();
			});

			return services;
		}
	}
}