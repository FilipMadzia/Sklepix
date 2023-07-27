using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklepix.Data.Entities;
using Sklepix.Models.Tasks;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	[Authorize]
	public class MyTasksController : Controller
	{
		private readonly TaskRepository _taskRepository;
		private readonly UserManager<UserEntity> _userManager;

		public MyTasksController(TaskRepository taskRepository, UserManager<UserEntity> userManager)
		{
			_taskRepository = taskRepository;
			_userManager = userManager;
		}

		string TaskStyleClass(TaskEntity taskEntity)
		{
			string styleClass = "";

			if(taskEntity.Status == TaskEntity.StatusEnum.Finished)
			{
				styleClass = "link-secondary";
			}
			else if((taskEntity.Deadline - DateTime.Now).TotalDays < 2)
			{
				styleClass = "link-danger";
			}
			else if((taskEntity.Deadline - DateTime.Now).TotalDays < 5)
			{
				styleClass = "link-warning";
			}

			return styleClass;
		}

		public IActionResult Index()
		{
			List<TaskDetailsViewModel> taskVms = _taskRepository.GetTasks()
				.OrderBy(x => x.Status)
				.Where(x => x.User == _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result)
				.Select(i => new TaskDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name,
					Description = i.Description,
					User = i.User,
					Deadline = i.Deadline,
					Priority = i.Priority,
					Status = i.Status,
					StyleClass = TaskStyleClass(i)
				})
				.ToList();

			return View(taskVms);
		}
	}
}
