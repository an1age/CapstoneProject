using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApi.Models.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>().HasKey(t => t.Id);

            modelBuilder.Entity<TodoList>().HasKey(tl => tl.Id);
        }
    }
}
