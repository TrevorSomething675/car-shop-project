using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
	public interface IRoleRepository
	{
		public Task<RoleEntity> GetRoleByNameAsync(string roleName);
	}
}
