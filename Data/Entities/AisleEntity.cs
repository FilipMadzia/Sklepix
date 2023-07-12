using System.ComponentModel;

namespace Sklepix.Data.Entities
{
    public class AisleEntity
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
    }
}
