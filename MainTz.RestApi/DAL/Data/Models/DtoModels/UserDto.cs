using Extensions;

namespace MainTz.RestApi.DAL.Data.Models.DtoModels
{
	public class UserDto
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public Roles Role { get; set; }
	}
}
