using Extensions;

namespace MainTz.RestApi.DAL.Data.Models.DtoModels
{
	public class UserDto
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public Roles Role { get; set; }
	}
}