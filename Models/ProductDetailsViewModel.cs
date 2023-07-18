using System.ComponentModel;

namespace Sklepix.Models
{
	public class ProductDetailsViewModel
	{
		public int Id { get; set; }
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
		[DisplayName("Ilość")]
		public int Count { get; set; }
        [DisplayName("Cena")]
        public decimal Price { get; set; }
		[DisplayName("Marża")]
        public string? Margin { get; set; }
		[DisplayName("Wartość bez marży")]
		public decimal TotalPrice { get; set; }
		[DisplayName("Wartość z marżą")]
		public decimal TotalPriceWithMargin { get; set; }
		[DisplayName("Kategoria")]
		public string? Category { get; set; }
		[DisplayName("Półka i alejka")]
		public string? ShelfAndAisle { get; set; }
	}
}
