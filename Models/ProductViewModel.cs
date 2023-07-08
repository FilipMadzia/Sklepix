using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public List<CategoryEntity>? Categories { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
