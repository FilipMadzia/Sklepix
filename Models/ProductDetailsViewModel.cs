using System.ComponentModel;

namespace Sklepix.Models
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        [DisplayName("Kategoria")]
        public string? Category { get; set; }
        [DisplayName("Półka i alejka")]
        public string? ShelfAndAisle { get; set; }
    }
}
