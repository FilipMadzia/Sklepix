namespace Sklepix.Data.Entities
{
	public class TaskEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public UserEntity? User { get; set; }
		public DateTime Deadline { get; set; }
		public int Priority { get; set; }
		public int Status { get; set; }
	}
}
