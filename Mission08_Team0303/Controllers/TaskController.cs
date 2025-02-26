using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;
using Mission08_Team0303.Repositories;  // ✅ Import the repository

namespace Mission08_Team0303.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;  // ✅ Inject repository
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddTask(task);  // ✅ Use repository to add task
                return RedirectToAction("Index");
            }

            return View(task);
        }
    }
}