using Sklepix.Data.Entities;
using System.ComponentModel;
using static Sklepix.Data.Entities.TaskEntity;

namespace Sklepix.Models.Tasks
{
	public class TaskDetailsViewModel
	{
		public int Id { get; set; }
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
		[DisplayName("Opis")]
		public string? Description { get; set; }
		[DisplayName("Pracownik")]
		public UserEntity? User { get; set; }
		[DisplayName("Termin")]
		public DateTime Deadline { get; set; }
		[DisplayName("Priorytet")]
		public PriorityEnum Priority { get; set; }
		[DisplayName("Status")]
		public StatusEnum Status { get; set; }
	}
}
