using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string? Name { get; set; }
    }
}
