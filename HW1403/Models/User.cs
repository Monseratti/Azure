using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1403.Models
{
	public class User
	{
		static int count = 0;
		[ValidateNever]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int RoleId { get; set; }

		public User()
		{
			Id = ++count;
		}
	}
}
