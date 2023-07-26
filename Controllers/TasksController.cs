using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models.Tasks;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class TasksController : Controller
	{
		private readonly TaskRepository _taskRepository;
		private readonly UserManager<UserEntity> _userManager;

		public TasksController(TaskRepository taskRepository, UserManager<UserEntity> userManager)
		{
			_taskRepository = taskRepository;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			List<TaskDetailsViewModel> taskVms= _taskRepository.GetTasks()
				.Select(i => new TaskDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name,
					Description = i.Description,
					User = i.User,
					Deadline = i.Deadline,
					Priority = i.Priority,
					Status = i.Status
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

			return View(taskVm);
		}

		public IActionResult Create()
		{
			TaskCreateViewModel taskVm = new TaskCreateViewModel()
			{
				Users = _userManager.Users.ToList()
			};

			return View(taskVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TaskCreateViewModel taskVm)
		{
			TaskEntity taskEntity = new TaskEntity()
			{
				Id = taskVm.Id,
				Name = taskVm.Name,
				Description = taskVm.Description,
				User = _userManager.FindByIdAsync(taskVm.UserId).Result,
				Deadline = taskVm.Deadline,
				Priority = taskVm.Priority,
				Status = TaskEntity.StatusEnum.Todo
			};

			if(taskEntity.User == null)
			{
				throw new Exception();
			}

			if(ModelState.IsValid)
			{
				_taskRepository.InsertTask(taskEntity);
				_taskRepository.Save();
				return RedirectToAction(nameof(Index));
			}

			taskVm.Users = _userManager.Users.ToList();

			return View(taskVm);
		}

		public IActionResult Edit(int id = -1)
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

			TaskCreateViewModel taskVm = new TaskCreateViewModel()
			{
				Id = taskEntity.Id,
				Name = taskEntity.Name,
				Description = taskEntity.Description,
				Users = _userManager.Users.ToList(),
				UserId = taskEntity.User.Id,
				Deadline = taskEntity.Deadline,
				Priority = taskEntity.Priority
			};

			return View(taskVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, TaskCreateViewModel taskVm)
		{
			TaskEntity taskEntity = new TaskEntity()
			{
				Id = taskVm.Id,
				Name = taskVm.Name,
				Description = taskVm.Description,
				Deadline = taskVm.Deadline,
				User = _userManager.FindByIdAsync(taskVm.UserId).Result,
				Priority = taskVm.Priority
			};

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

			taskVm.Users = _userManager.Users.ToList();

			return View(taskVm);
		}

		public IActionResult Delete(int id = -1)
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

			return View(taskVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if(_taskRepository == null)
			{
				return Problem("Entity set 'SklepixContext.TaskEntity'  is null.");
			}

			TaskEntity taskEntity = _taskRepository.GetTaskById(id);

			if(taskEntity != null)
			{
				_taskRepository.DeleteTask(id);
			}

			_taskRepository.Save();
			return RedirectToAction(nameof(Index));
		}
	}
}
