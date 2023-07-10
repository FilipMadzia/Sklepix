using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public List<CategoryEntity>? Categories { get; set; }
        public int CategoryId { get; set; }
        public List<ShelfEntity>? Shelves { get; set; }
        public int ShelfId { get; set; }
    }
}
