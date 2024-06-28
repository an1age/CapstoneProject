using System.ComponentModel.DataAnnotations;

namespace TodoListApp.WebApi.Models.Entities
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
