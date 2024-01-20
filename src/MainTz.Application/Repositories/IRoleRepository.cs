using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IRoleRepository
	{
		public Task<Role> GetRoleByNameAsync(string roleName);
	}
}
