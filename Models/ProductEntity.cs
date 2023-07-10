using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        [DisplayName("Kategoria")]
        public CategoryEntity? Category { get; set; }
        public ShelfEntity? Shelf { get; set; }
    }
}
