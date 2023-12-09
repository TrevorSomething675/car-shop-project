using System.ComponentModel.DataAnnotations;

namespace MainTz.RestApi.DAL.Data.Models.DtoModels
{
	public class UserDto
	{
        public int Id { get; set; }

		[Required]
		[EmailAddress]
		public string Name { get; set; }

		[Required]
		[MinLength(6, ErrorMessage = "Пароль слишком короткий")]
		[MaxLength(20, ErrorMessage = "Пароль слишком дилнный")]
		public string Password { get; set; }

        public string Role { get; set; }
	}
}