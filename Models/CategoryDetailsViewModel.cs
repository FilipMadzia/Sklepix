using System.ComponentModel;

namespace Sklepix.Models
{
	public class CategoryDetailsViewModel
	{
		public int Id { get; set; }
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
	}
}
