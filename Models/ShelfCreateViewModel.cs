using System.ComponentModel;
using Sklepix.Data.Entities;

namespace Sklepix.Models
{
    public class ShelfCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Numer")]
        public int Number { get; set; }
        public List<AisleEntity>? Aisles { get; set; }
        [DisplayName("Alejka")]
        public int AisleId { get; set; }
    }
}
