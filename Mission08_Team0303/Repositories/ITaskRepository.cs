using System.Collections.Generic;

namespace Mission08_Team0303.Repositories  // ✅ Updated namespace
{
    public interface ITaskRepository
    {
        IEnumerable<Mission08_Team0303.Models.ToDoTask> GetIncompleteTasks();
        Mission08_Team0303.Models.ToDoTask GetTaskById(int id);
        void AddTask(Mission08_Team0303.Models.ToDoTask task);
        void UpdateTask(Mission08_Team0303.Models.ToDoTask task);
        void DeleteTask(int id);
        void Save();
    }
}