using System.Collections.Generic;
using Mission08_Team0303.Models;

namespace Mission08_Team0303.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<ToDoTask> GetIncompleteTasks();
        ToDoTask GetTaskById(int id);
        void AddTask(ToDoTask task);
        void UpdateTask(ToDoTask task);
        void DeleteTask(int id);
        IEnumerable<Category> GetCategories(); // ✅ Added method to get categories
        void Save();
        IEnumerable<ToDoTask> GetTasks(); // ✅ Add this method
    }
}