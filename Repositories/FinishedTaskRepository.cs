using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Data;

namespace Sklepix.Repositories
{
	public class FinishedTaskRepository : IFinishedTaskRepository
	{
		private readonly SklepixContext _context;

		public FinishedTaskRepository(SklepixContext context)
		{
			_context = context;
		}

		public List<FinishedTaskEntity> GetFinishedTasks()
		{
			return _context.FinishedTaskEntity
				.Include(m => m.Task)
				.ToList();
		}

		public FinishedTaskEntity GetFinishedTaskById(int id)
		{
			return _context.FinishedTaskEntity
				.Include(m => m.Task)
				.First(x => x.Id == id);
		}

		public void InsertFinishedTask(FinishedTaskEntity finishedTaskEntity)
		{
			_context.Add(finishedTaskEntity);
		}

		public void DeleteFinishedTask(int id)
		{
			_context.Remove(_context.FinishedTaskEntity.Find(id));
		}

		public void UpdateFinishedTask(FinishedTaskEntity finishedTaskEntity)
		{
			_context.Entry(finishedTaskEntity).State = EntityState.Modified;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
