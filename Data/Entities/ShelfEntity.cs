namespace Sklepix.Data.Entities
{
	public class ShelfEntity
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public AisleEntity? Aisle { get; set; }
	}
}
