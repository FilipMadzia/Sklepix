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
        public int AisleId { get; set; }
    }
}
