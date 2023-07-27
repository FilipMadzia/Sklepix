using Sklepix.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models.Products
{
	public class ProductCreateViewModel
	{
		public int Id { get; set; }
		[Required]
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
		[Range(1, 999, ErrorMessage = "Ilość nie może być mniejsza od 1 ani większa od 999")]
		[Required]
		[DisplayName("Ilość")]
		public int Count { get; set; }
		[Range(1, 999, ErrorMessage = "Cena nie może być mniejsza od 1 ani większa od 999")]
		[Required]
		[DisplayName("Cena")]
		public decimal Price { get; set; }
		[Range(1, 999, ErrorMessage = "Marża nie może być mniejsza od 1 ani większa od 999")]
		[Required]
		[DisplayName("Marża (w procentach)")]
		public float Margin { get; set; }
		public List<CategoryEntity>? Categories { get; set; }
		[DisplayName("Kategoria")]
		public int CategoryId { get; set; }
		public List<ShelfEntity>? Shelves { get; set; }
		[DisplayName("Półka")]
		public int ShelfId { get; set; }
		public List<AisleEntity>? Aisles { get; set; }
	}
}
