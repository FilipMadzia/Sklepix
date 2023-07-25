using Microsoft.AspNetCore.Identity;

namespace Sklepix.Data.Entities
{
	public class UserEntity : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime CreationTime { get; set; }
	}
}
