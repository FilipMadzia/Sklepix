using System.ComponentModel;

namespace Sklepix.Models
{
    public class ShelfDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Number")]
        public int Number { get; set; }
        [DisplayName("Alejka")]
        public string? Aisle { get; set; }
    }
}
