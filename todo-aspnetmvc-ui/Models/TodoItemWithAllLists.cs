using System.Collections.Generic;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Models
{
    public class TodoItemWithAllLists : TaskItem
    {
        public List<TaskList> TaskLists { get; set; }
    }
}
