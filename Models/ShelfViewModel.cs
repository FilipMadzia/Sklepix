namespace Sklepix.Models
{
    public class ShelfViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<AisleEntity>? Aisles { get; set; }
        public int AisleId { get; set; }
    }
}
