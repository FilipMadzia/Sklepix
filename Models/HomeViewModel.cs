using System.ComponentModel;

namespace Sklepix.Models
{
	public class HomeViewModel
	{
		[DisplayName("Ilość produktów")]
		public int ProductCount { get; set; }
		[DisplayName("Ilość kategorii")]
		public int CategoryCount { get; set; }
		[DisplayName("Ilość alejek")]
		public int AisleCount { get; set; }
		[DisplayName("Ilość półek")]
		public int ShelfCount { get; set; }
	}
}
