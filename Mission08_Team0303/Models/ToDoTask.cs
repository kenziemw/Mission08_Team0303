using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team0303.Models
{
    public enum Quadrant
    {
        ImportantUrgent = 1,
        ImportantNotUrgent = 2,
        NotImportantUrgent = 3,
        NotImportantNotUrgent = 4
    }

    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public Quadrant Quadrant { get; set; }  // ✅ Changed from string to Enum

        [Required]
        public int CategoryId { get; set; }  // ✅ Changed from string to Foreign Key

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }  // ✅ Foreign Key Relationship

        public bool Completed { get; set; }
    }
}