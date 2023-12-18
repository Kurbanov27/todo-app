using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo_domain_entities.Data;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Models;

namespace todo_domain_entities.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TodoContext _context;

        public TasksRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAll()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetById(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<List<TaskList>> GetAllTaskLists()
        {
            return await _context.TaskLists.ToListAsync();
        }

        public void Add(TaskItem task)
        {
            _context.Add(task);
        }

        public void Update(TaskItem task)
        {
            _context.Update(task);
        }

        public void Delete(TaskItem task)
        {
            _context.TaskItems.Remove(task);
        }

        public void DeleteTasks(IEnumerable<TaskItem> tasks)
        {
            _context.TaskItems.RemoveRange(tasks);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool TaskExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
