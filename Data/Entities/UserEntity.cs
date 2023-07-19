using Microsoft.AspNetCore.Identity;

namespace Sklepix.Data.Entities
{
	public class UserEntity : IdentityUser
	{
		public DateTime CreationTime { get; set; }
	}
}
