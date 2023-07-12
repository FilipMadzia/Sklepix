using Sklepix.Data.Entities;
using System.ComponentModel;

namespace Sklepix.Models
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        public List<CategoryEntity>? Categories { get; set; }
        public int CategoryId { get; set; }
        public List<ShelfEntity>? Shelves { get; set; }
        public int ShelfId { get; set; }
        public List<AisleEntity>? Aisles { get; set; }
    }
}
