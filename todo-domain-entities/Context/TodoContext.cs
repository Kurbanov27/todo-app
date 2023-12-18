using Microsoft.EntityFrameworkCore;
using todo_domain_entities.Models;

namespace todo_domain_entities.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public virtual DbSet<TaskItem> TaskItems { get; set; }
        public virtual DbSet<TaskList> TaskLists {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasOne(p => p.TodoList)
                .WithMany(b => b.TodoItems)
                .HasForeignKey(x => x.TodoListId);

            modelBuilder.Entity<TaskItem>().ToTable("TodoItems");
            modelBuilder.Entity<TaskList>().ToTable("TodoLists");
        }
    }
}
