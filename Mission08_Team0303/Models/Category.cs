using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0303.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}