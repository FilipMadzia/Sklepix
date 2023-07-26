using Sklepix.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Sklepix.Data.Entities.TaskEntity;

namespace Sklepix.Models.Tasks
{
	public class TaskCreateViewModel
	{
		public int Id { get; set; }
		[Required]
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
		[DisplayName("Opis")]
		public string? Description { get; set; }
		public List<UserEntity>? Users { get; set; }
		[DisplayName("Pracownik")]
		public string? UserId { get; set; }
		[DisplayName("Termin")]
		public DateTime Deadline { get; set; }
		[DisplayName("Priorytet")]
		public PriorityEnum Priority { get; set; }
	}
}
