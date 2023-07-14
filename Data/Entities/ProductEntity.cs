namespace Sklepix.Data.Entities
{
	public class ProductEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
		public int Count { get; set; }
		public CategoryEntity? Category { get; set; }
		public ShelfEntity? Shelf { get; set; }
	}
}
