using System.Collections.Generic;
using System.Threading.Tasks;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Interfaces
{
    public interface ITasksService
    {
        Task<List<TaskItem>> GetAllTasks();

        Task<TaskItem> GetTaskById(int id);

        Task<List<TaskList>> GetAllTaskLists();

        Task CreateTask(TaskItem task);

        Task UpdateTask(TaskItem task);

        Task DeleteTask(TaskItem task);

        Task DeleteCompletedTasks();

        bool TaskExists(int id);
    }
}
