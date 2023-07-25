using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
	public interface IFinishedTaskRepository
	{
		List<FinishedTaskEntity> GetFinishedTasks();
		FinishedTaskEntity GetFinishedTaskById(int id);
		void InsertFinishedTask(FinishedTaskEntity finishedTaskEntity);
		void DeleteFinishedTask(int id);
		void UpdateFinishedTask(FinishedTaskEntity finishedTaskEntity);
		void Save();
	}
}
