using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Interfaces;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Services
{
    public class ListsService : IListsService
    {
        private readonly IListsRepository _repository;

        public ListsService(IListsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskList>> GetAllTaskLists()
        {
            return await _repository.GetAll();
        }

        public async Task<TaskList> GetTaskListById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task CreateTaskList(TaskList taskList)
        {
            _repository.Add(taskList);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTaskList(TaskList taskList)
        {
            var existingTaskList = await _repository.GetById(taskList.Id);

            if (existingTaskList == null)
            {
                throw new ArgumentException($"TaskList with id {taskList.Id} not found");
            }

            existingTaskList.Title = taskList.Title;

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteTaskList(TaskList taskList)
        {
            _repository.Delete(taskList);
            await _repository.SaveChangesAsync();
        }

        public bool TaskListExists(int id)
        {
            return _repository.TaskListExists(id);
        }
    }
}
