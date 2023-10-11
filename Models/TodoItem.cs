using System.ComponentModel.DataAnnotations;
using TodoApp.Models.Validations;

namespace TodoApp.Models
{
    public class TodoItem
    {
        [Key]
        public Guid TodoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Due date is required")]
        [FutureDate(ErrorMessage = "Due date must be a future date")]
        public DateTime DueDate { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pending; 
    }
}
