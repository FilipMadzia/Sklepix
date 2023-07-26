using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		//public async Task<IActionResult> Edit(int? id)
		//{
		//	if(id == null || _context.TaskEntity == null)
		//	{
		//		return NotFound();
		//	}

		//	var taskEntity = await _context.TaskEntity.FindAsync(id);
		//	if(taskEntity == null)
		//	{
		//		return NotFound();
		//	}
		//	return View(taskEntity);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Deadline,Priority,Status")] TaskEntity taskEntity)
		//{
		//	if(id != taskEntity.Id)
		//	{
		//		return NotFound();
		//	}

		//	if(ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_context.Update(taskEntity);
		//			await _context.SaveChangesAsync();
		//		}
		//		catch(DbUpdateConcurrencyException)
		//		{
		//			if(!TaskEntityExists(taskEntity.Id))
		//			{
		//				return NotFound();
		//			}
		//			else
		//			{
		//				throw;
		//			}
		//		}
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(taskEntity);
		//}

		//public async Task<IActionResult> Delete(int? id)
		//{
		//	if(id == null || _context.TaskEntity == null)
		//	{
		//		return NotFound();
		//	}

		//	var taskEntity = await _context.TaskEntity
		//		.FirstOrDefaultAsync(m => m.Id == id);
		//	if(taskEntity == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(taskEntity);
		//}

		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	if(_context.TaskEntity == null)
		//	{
		//		return Problem("Entity set 'SklepixContext.TaskEntity'  is null.");
		//	}
		//	var taskEntity = await _context.TaskEntity.FindAsync(id);
		//	if(taskEntity != null)
		//	{
		//		_context.TaskEntity.Remove(taskEntity);
		//	}

		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}
	}
}
