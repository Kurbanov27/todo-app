using todo_domain_entities.Models;

namespace todo_domain_entities.Interfaces
{
    public interface IListsRepository : IBaseRepository<TaskList>
    {
        bool TaskListExists(int id);
    }
}
