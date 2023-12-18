using System;
using System.ComponentModel.DataAnnotations;
using todo_domain_entities.Enums;

namespace todo_domain_entities.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [NotPastDate]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskItemStatus Status { get; set; }

        public int TodoListId { get; set; }

        public TaskList TodoList { get; set; }
    }
}
