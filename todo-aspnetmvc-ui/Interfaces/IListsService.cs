using System.Collections.Generic;
using System.Threading.Tasks;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Interfaces
{
    public interface IListsService
    {
        Task<List<TaskList>> GetAllTaskLists();

        Task<TaskList> GetTaskListById(int id);

        Task CreateTaskList(TaskList taskList);

        Task UpdateTaskList(TaskList taskList);

        Task DeleteTaskList(TaskList taskList);

        bool TaskListExists(int id);
    }
}
