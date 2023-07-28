using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
				styleClass = "link-warning-dark";
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

		public IActionResult Details(int id = -1)
		{
			if(id == -1 || _taskRepository == null)
			{
				return NotFound();
			}

			TaskEntity taskEntity = _taskRepository.GetTaskById(id);

			if(taskEntity == null)
			{
				return NotFound();
			}

			TaskDetailsViewModel taskVm = new TaskDetailsViewModel()
			{
				Id = taskEntity.Id,
				Name = taskEntity.Name,
				Description = taskEntity.Description,
				User = taskEntity.User,
				Deadline = taskEntity.Deadline,
				Priority = taskEntity.Priority,
				Status = taskEntity.Status
			};

			if(taskEntity.Status == TaskEntity.StatusEnum.Finished)
			{
				taskVm.FinishedTime = taskEntity.FinishedTime;
				taskVm.IsFinishedSuccessfully = taskEntity.IsFinishedSuccessfully == true ? "Tak" : "Nie";
				taskVm.Notes = taskEntity.Notes;
				taskVm.IsCompleted = true;
			}
			else
			{
				taskVm.IsCompleted = false;
			}

			return View(taskVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Details(int id, TaskDetailsViewModel taskVm)
		{
			TaskEntity taskEntity = _taskRepository.GetTaskById(id);

			switch(taskEntity.Status)
			{
				case TaskEntity.StatusEnum.Todo:
					taskEntity.Status = TaskEntity.StatusEnum.Doing;
					break;
				case TaskEntity.StatusEnum.Doing:
					taskEntity.FinishedTime = DateTime.Now;
					taskEntity.IsFinishedSuccessfully = Request.Form["IsFinishedSuccessfully"].ToString() == "on";
					taskEntity.Notes = taskVm.Notes;
					taskEntity.Status = TaskEntity.StatusEnum.Finished;
					break;
				default:
					break;
			}

			if(id != taskEntity.Id)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					_taskRepository.UpdateTask(taskEntity);
					_taskRepository.Save();
				}
				catch(DbUpdateConcurrencyException)
				{
					if(_taskRepository.GetTaskById(id) == null)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Details), new { id = id });
			}

			return View(taskVm);
		}
	}
}
