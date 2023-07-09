using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
    }
}
