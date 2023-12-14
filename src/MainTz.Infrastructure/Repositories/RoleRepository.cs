using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly MainContext _mainContext;
		public RoleRepository(MainContext mainContext)
		{
			_mainContext = mainContext;
		}
		public async Task<RoleEntity> GetRoleByName(string roleName)
		{
			var resultRole = await _mainContext.Roles.FirstOrDefaultAsync(role => role.RoleName == roleName);
			return resultRole;
		}
	}
}