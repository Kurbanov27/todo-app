using System.Collections.Generic;
using System.Threading.Tasks;
using todo_domain_entities.Models;

namespace todo_domain_entities.Interfaces
{
    public interface ITasksRepository : IBaseRepository<TaskItem>
    {
        Task<List<TaskList>> GetAllTaskLists();

        void DeleteTasks(IEnumerable<TaskItem> tasks);

        bool TaskExists(int id);
    }
}
