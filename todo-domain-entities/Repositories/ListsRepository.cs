using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo_domain_entities.Data;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Models;

namespace todo_domain_entities.Repositories
{
    public class ListsRepository : IListsRepository
    {
        private readonly TodoContext _context;

        public ListsRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TaskList>> GetAll()
        {
            return await _context.TaskLists.ToListAsync();
        }

        public async Task<TaskList> GetById(int id)
        {
            return await _context.TaskLists.FindAsync(id);
        }

        public void Add(TaskList taskList)
        {
            _context.Add(taskList);
        }

        public void Update(TaskList taskList)
        {
            _context.Update(taskList);
        }

        public void Delete(TaskList taskList)
        {
            var list = _context.TaskLists.Find(taskList.Id);

            _context.TaskLists.Remove(list);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool TaskListExists(int id)
        {
            return _context.TaskLists.Any(e => e.Id == id);
        }
    }
}
