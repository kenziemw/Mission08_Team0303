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

            _taskRepository.AddTask(viewModel.Task);
            return RedirectToAction("Index", "Task");
        }

        // 📌 Update task (for inline AJAX save)
        [HttpPost]
        public IActionResult UpdateTask([FromBody] ToDoTask task)
        {
            try
            {
                // ✅ Log the entire received JSON object
                Console.WriteLine("📥 Received Task Update Request:");
                Console.WriteLine($"   🔹 JSON Data: {System.Text.Json.JsonSerializer.Serialize(task)}");

                if (task == null)
                {
                    Console.WriteLine("❌ Error: Received NULL Task object.");
                    return BadRequest(new { message = "Invalid task data. Task object is missing." });
                }

                if (task.Id == 0)
                {
                    Console.WriteLine("❌ Error: Task ID is missing or zero.");
                    return BadRequest(new { message = "Invalid task data. Task ID is missing or invalid." });
                }

                var existingTask = _taskRepository.GetTaskById(task.Id);
                if (existingTask == null)
                {
                    Console.WriteLine($"❌ Error: Task with ID {task.Id} not found.");
                    return NotFound(new { message = $"Task with ID {task.Id} not found." });
                }

                // ✅ Log old vs new task data
                Console.WriteLine($"🔄 Updating Task {task.Id}:");
                Console.WriteLine($"   📝 Old Name: {existingTask.Name}  →  New Name: {task.Name}");
                Console.WriteLine($"   📅 Old Due Date: {existingTask.DueDate}  →  New Due Date: {task.DueDate}");
                Console.WriteLine($"   📌 Old Quadrant: {existingTask.Quadrant}  →  New Quadrant: {task.Quadrant}");
                Console.WriteLine($"   ✅ Old Completed: {existingTask.Completed}  →  New Completed: {task.Completed}");

                // ✅ Update task properties
                existingTask.Name = task.Name ?? existingTask.Name;
                existingTask.DueDate = task.DueDate ?? existingTask.DueDate;
                existingTask.CategoryId = task.CategoryId != 0 ? task.CategoryId : existingTask.CategoryId;
                existingTask.Quadrant = task.Quadrant;
                existingTask.Completed = task.Completed;

                _taskRepository.UpdateTask(existingTask);

                Console.WriteLine($"✅ Task {task.Id} updated successfully.");

                return Ok(new { message = "Task updated successfully", updatedTask = existingTask });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Server Error: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        // 📌 Delete task (for inline AJAX delete)
        [HttpPost] // Change to [HttpDelete] if frontend supports DELETE requests
        public IActionResult DeleteTask([FromBody] ToDoTask task)
        {
            try
            {
                if (task == null || task.Id == 0)
                {
                    Console.WriteLine("❌ Error: Invalid task data received for delete.");
                    return BadRequest(new { message = "Invalid task data. Task ID is missing." });
                }

                var existingTask = _taskRepository.GetTaskById(task.Id);
                if (existingTask == null)
                {
                    Console.WriteLine($"❌ Error: Task with ID {task.Id} not found.");
                    return NotFound(new { message = $"Task with ID {task.Id} not found." });
                }

                _taskRepository.DeleteTask(task.Id);
                Console.WriteLine($"🗑 Task {task.Id} deleted successfully.");

                return Ok(new { message = "Task deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Server Error during delete: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
