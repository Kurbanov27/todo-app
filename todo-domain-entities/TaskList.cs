using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todo_domain_entities.Models
{
    public class TaskList
    {
        public int Id { get; set; }

        [StringLength(16, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }

        public virtual ICollection<TaskItem> TodoItems { get; set; } = new List<TaskItem>();
    }
}
