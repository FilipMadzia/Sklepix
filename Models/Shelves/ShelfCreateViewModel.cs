using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sklepix.Data.Entities;

namespace Sklepix.Models.Shelves
{
	public class ShelfCreateViewModel
	{
		public int Id { get; set; }
		[Range(1, 999, ErrorMessage = "Numer półki nie może być mniejszy od 1 ani większy od 999")]
		[Required]
		[DisplayName("Numer")]
		public int Number { get; set; }
		public List<AisleEntity>? Aisles { get; set; }
		[DisplayName("Alejka")]
		public int AisleId { get; set; }
	}
}
