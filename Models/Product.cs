using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Cena jest wymagana")]
        [RegularExpression(@"\d{1,9}[\.,\,]\d{2}", ErrorMessage = "Podaj właściwą cenę")]
        public decimal ProductPrice { get; set; }

        public Product()
        {
            
        }
    }
}
