using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Data.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
    }
}
