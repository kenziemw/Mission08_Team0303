using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_Team0303.Models;
using Mission08_Team0303.Models.ViewModels;
using Mission08_Team0303.Repositories;
using System;
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

        // 📌 Display the task quadrants
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetTasks() ?? new List<ToDoTask>(); // Ensures it's never null
            return View(tasks);
        }

        // 📌 Create task form (GET)
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

        // 📌 Create task (POST)
        [HttpPost]
        public IActionResult Create(TaskViewModel viewModel)
        {
            // Ensure categories are always populated
            viewModel.Categories = _taskRepository.GetCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _taskRepository.AddTask(viewModel.Task);
            _taskRepository.Save();

            // ✅ Redirect back to Index after creating the task
            return RedirectToAction("Index", "Task");
        }


        // 📌 Edit task (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new TaskViewModel
            {
                Task = task,
                Categories = _taskRepository.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
            };

            return View(viewModel);
        }
        
        // 📌 Edit task (POST)
        [HttpPost]
        public IActionResult Edit(TaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _taskRepository.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

                return View(viewModel);
            }

            var existingTask = _taskRepository.GetTaskById(viewModel.Task.Id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Name = viewModel.Task.Name;
            existingTask.DueDate = viewModel.Task.DueDate;
            existingTask.CategoryId = viewModel.Task.CategoryId;
            existingTask.Quadrant = viewModel.Task.Quadrant;
            existingTask.Completed = viewModel.Task.Completed;

            _taskRepository.UpdateTask(existingTask);
            _taskRepository.Save();

            return RedirectToAction("Index", "Task");
        }

        // 📌 Delete task (POST)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound(); // Return a 404 if the task doesn't exist
            }

            _taskRepository.DeleteTask(id);
            _taskRepository.Save(); // Ensure changes are saved

            return RedirectToAction("Index");
        }
    }
}
