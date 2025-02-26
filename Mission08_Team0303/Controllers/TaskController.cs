using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;
using Mission08_Team0303.Repositories;  // ✅ Import the repository to interact with tasks

namespace Mission08_Team0303.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        // ✅ Constructor to inject the task repository dependency
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;  // ✅ Store the injected repository instance
        }

        [HttpGet]
        public IActionResult Create()
        {
            // ✅ Return the view for creating a new task
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDoTask task)
        {
            // ✅ Check if the task model is valid before proceeding
            if (ModelState.IsValid)
            {
                _taskRepository.AddTask(task);  // ✅ Add the new task using the repository
                return RedirectToAction("Index");  // ✅ Redirect to the Index view after saving
            }

            // ✅ If the model is invalid, reload the Create view with the entered data
            return View(task);
        }
    }
}