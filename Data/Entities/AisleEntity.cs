namespace Sklepix.Data.Entities
{
	public class AisleEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public UserEntity? User { get; set; }
	}
}
