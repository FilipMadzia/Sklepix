using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
	public class TaskRepository : ITaskRepository
	{
		private readonly SklepixContext _context;

		public TaskRepository(SklepixContext context)
		{
			_context = context;
		}

		public List<TaskEntity> GetTasks()
		{
			return _context.TaskEntity
				.Include(m => m.User)
				.ToList();
		}

		public TaskEntity GetTaskById(int id)
		{
			return _context.TaskEntity
				.Include(m => m.User)
				.First(x => x.Id == id);
		}

		public void InsertTask(TaskEntity taskEntity)
		{
			_context.Add(taskEntity);
		}

		public void DeleteTask(int id)
		{
			_context.Remove(_context.TaskEntity.Find(id));
		}

		public void UpdateTask(TaskEntity taskEntity)
		{
			_context.Entry(taskEntity).State = EntityState.Modified;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
