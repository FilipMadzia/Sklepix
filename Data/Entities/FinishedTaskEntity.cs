namespace Sklepix.Data.Entities
{
	public class FinishedTaskEntity
	{
		public int Id { get; set; }
		public TaskEntity? Task { get; set; }
		public DateTime FinishedTime { get; set; }
		public bool IsFinishedSuccessfully { get; set; }
		public string? Notes { get; set; }
	}
}
