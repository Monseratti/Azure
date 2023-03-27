using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1403.Models
{
	public class Role
	{
		static int count = 0;
		[ValidateNever]
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<User> Users { get; set; }

		public Role() 
		{
			Id = ++count;
			Users = new List<User>();
		}
	}
}
