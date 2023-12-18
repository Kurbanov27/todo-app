using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Interfaces;
using todo_domain_entities.Enums;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repository;

        public TasksService(ITasksRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            return await _repository.GetAll();
        }

        public async Task<TaskItem> GetTaskById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<TaskList>> GetAllTaskLists()
        {
            return await _repository.GetAllTaskLists();
        }

        public async Task CreateTask(TaskItem task)
        {
            _repository.Add(task);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskItem task)
        {
            var existingTask = await _repository.GetById(task.Id);

            if (existingTask == null)
            {
                throw new ArgumentException($"Task with id {task.Id} not found");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.TodoListId = task.TodoListId;
            existingTask.Status = task.Status;

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteTask(TaskItem task)
        {
            _repository.Delete(task);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteCompletedTasks()
        {
            var completedTasks = (await _repository.GetAll()).Where(t => t.Status == TaskItemStatus.Completed);

            _repository.DeleteTasks(completedTasks);
            await _repository.SaveChangesAsync();
        }

        public bool TaskExists(int id)
        {
            return _repository.TaskExists(id);
        }
    }
}
