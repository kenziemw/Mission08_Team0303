namespace Mission08_Team0303.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public string Quadrant { get; set; }
        public string Category { get; set; }
        public bool Completed { get; set; }
    }
}