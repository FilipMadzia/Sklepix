namespace Sklepix.Data.Entities
{
	public class TaskEntity
	{
		public enum PriorityEnum
		{
			Low,
			Medium,
			High
		}
		public enum StatusEnum
		{
			Todo,
			Doing,
			Finished
		}

		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public UserEntity? User { get; set; }
		public DateTime Deadline { get; set; }
		public PriorityEnum Priority { get; set; }
		public StatusEnum Status { get; set; }
		public DateTime FinishedTime { get; set; }
		public bool IsFinishedSuccessfully { get; set; }
		public string? Notes { get; set; }
	}
}
