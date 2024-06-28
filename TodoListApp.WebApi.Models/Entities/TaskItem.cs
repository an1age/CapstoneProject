using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.WebApi.Models.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }

        [ForeignKey("TodoList")]
        public int TodoListId { get; set; }
    }
}
