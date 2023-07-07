using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Cena jest wymagana")]
        public decimal Price { get; set; }

        public ProductEntity()
        {
            
        }
    }
}
