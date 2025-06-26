using System.ComponentModel.DataAnnotations;

namespace TodoApp.Contracts
{
    public class CreateTodoRequest
    {
        [Required]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }
    }
}
