using System.ComponentModel;

namespace Sklepix.Data.Entities
{
    public class ShelfEntity
    {
        public int Id { get; set; }
        [DisplayName("Numer")]
        public int Number { get; set; }
        [DisplayName("Alejka")]
        public AisleEntity? Aisle { get; set; }
    }
}
