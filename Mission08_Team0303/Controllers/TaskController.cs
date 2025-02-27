using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_Team0303.Models;
using Mission08_Team0303.Models.ViewModels;
using Mission08_Team0303.Repositories;
using System.Linq;

namespace Mission08_Team0303.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IActionResult Index()
        {
            var tasks = _taskRepository.GetTasks();
            return View(Tasks);
        }

        public string? Tasks { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new TaskViewModel
            {
                Task = new ToDoTask(),
                Categories = _taskRepository.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel viewModel)
        {
            _taskRepository.AddTask(viewModel.Task);
            return RedirectToAction("Index", "Task");
        }
        
        [HttpPost]
        public IActionResult UpdateTask([FromBody] ToDoTask task)
        {
            if (task == null)
            {
                return BadRequest("Invalid task data");
            }

            var existingTask = _taskRepository.GetTaskById(task.Id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Name = task.Name;
            existingTask.DueDate = task.DueDate;
            existingTask.CategoryId = task.CategoryId;
            existingTask.Quadrant = task.Quadrant;
            existingTask.Completed = task.Completed;

            _taskRepository.UpdateTask(existingTask);

            return Ok();
        }
    }
}