using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
	public class UserCreateViewModel
	{
		public string? Id { get; set; }
		public DateTime CreationTime { get; set; }
		[Required]
		[DisplayName("Nazwa użytkownika")]
		public string? UserName { get; set; }
		[Required]
		[DisplayName("Email")]
		public string? Email { get; set; }
		[Required]
		[DisplayName("Nr telefonu")]
		public string? PhoneNumber { get; set; }
		[Required]
		[DisplayName("Hasło")]
		public string? Password { get; set; }
		[DisplayName("Uprawnienia")]
		public List<IdentityRole>? Roles { get; set; }
		public string? RolesString { get; set; }
	}
}
