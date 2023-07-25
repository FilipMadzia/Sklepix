using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
	public interface ITaskRepository
	{
		List<TaskEntity> GetTasks();
		TaskEntity GetTaskById(int id);
		void InsertTask(TaskEntity taskEntity);
		void DeleteTask(int id);
		void UpdateTask(TaskEntity taskEntity);
		void Save();
	}
}
