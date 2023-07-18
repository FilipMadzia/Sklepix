using Microsoft.AspNetCore.Identity;

namespace Sklepix.Data.Entities
{
	public class UserEntity : IdentityUser
	{
		public string? Name { get; set; }
	}
}
