using System.ComponentModel;

namespace Sklepix.Models
{
	public class UserViewModel
	{
		public string? Id { get; set; }
		[DisplayName("Data dodania")]
		public DateTime CreationTime { get; set; }
		[DisplayName("Imię")]
		public string? UserName { get; set; }
		[DisplayName("Email")]
		public string? Email { get; set; }
		[DisplayName("Nr telefonu")]
		public string? PhoneNumber { get; set; }
		[DisplayName("Uprawnienia")]
		public List<string>? Roles { get; set; }
	}
}
