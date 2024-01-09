using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		public RoleRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}
		public async Task<RoleEntity> GetRoleByNameAsync(string roleName)
		{
			using(var context = _dbContextFactory.CreateDbContext())
			{
				var resultRole = await context.Roles.FirstOrDefaultAsync(role => role.RoleName == roleName);
				return resultRole;
			}
		}
	}
}