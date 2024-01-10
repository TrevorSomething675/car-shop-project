using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Repositories
{
	public interface IRoleRepository
	{
		public Task<Role> GetRoleByNameAsync(string roleName);
	}
}
