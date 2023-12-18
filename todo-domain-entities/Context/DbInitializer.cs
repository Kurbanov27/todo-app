using System;
using System.Linq;
using todo_domain_entities.Models;

namespace todo_domain_entities.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TodoContext context)
        {
            context.Database.EnsureCreated();

            if (!context.TaskLists.Any())
            {
                var lists = new TaskList[]
                {
                    new TaskList { Title = "Today"},
                    new TaskList { Title = "Planned" }
                };

                foreach (var list in lists)
                {
                    context.TaskLists.Add(list);
                }

                context.SaveChanges();
            }

            if (!context.TaskItems.Any())
            {
                var items = new TaskItem[]
                {
                    new TaskItem { Title = "test", Description = "test", CreatedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1).Date, Status = Enums.TaskItemStatus.Active, TodoListId = 1},
                    new TaskItem { Title = "test2", Description = "test2", CreatedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1).Date, Status = Enums.TaskItemStatus.Active, TodoListId = 1}
                };

                foreach (var item in items)
                {
                    context.TaskItems.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
