using System.Collections.Generic;
using Mission08_Team0303.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission08_Team0303.Models.ViewModels
{
    public class TaskViewModel
    {
        public ToDoTask Task { get; set; } = new ToDoTask(); // ✅ Ensure it's initialized
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>(); // ✅ Prevent null
    }
}