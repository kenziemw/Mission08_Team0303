using System.Collections.Generic;
using System.Linq;
using Mission08_Team0303.Data;
using Mission08_Team0303.Models;

namespace Mission08_Team0303.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDoTask> GetIncompleteTasks()
        {
            return _context.Tasks.Where(t => !t.Completed).ToList();
        }

        public ToDoTask GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void AddTask(ToDoTask task)
        {
            _context.Tasks.Add(task);
            Save();
        }
        
        public IEnumerable<ToDoTask> GetTasks() // ✅ Implemented method
        {
            return _context.Tasks.ToList();
        }

        public void UpdateTask(ToDoTask task)
        {
            _context.Tasks.Update(task);
            Save();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                Save();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList(); // ✅ Fetch categories from the database
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}