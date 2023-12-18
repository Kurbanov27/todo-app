using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo_aspnetmvc_ui.Interfaces;
using todo_aspnetmvc_ui.Models;
using todo_domain_entities.Enums;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Controllers
{
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITasksService _tasksService;

        public TaskController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _tasksService.GetAllTasks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _tasksService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var todoItemWithAllLists = new TodoItemWithAllLists
            {
                TaskLists = await _tasksService.GetAllTaskLists()
            };

            return View(todoItemWithAllLists);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate,TodoListId,Status")] TodoItemWithAllLists task)
        {
            if (ModelState.IsValid)
            {
                var taskItem = (TaskItem)task;

                taskItem.CreatedDate = DateTime.Now;

                await _tasksService.CreateTask(taskItem);

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return View(task);
        }

        [HttpGet("filter/{status}")]
        public async Task<IActionResult> FilterByStatus(TaskItemStatus status)
        {
            return View("Index", (await _tasksService.GetAllTasks()).Where(t => t.Status == status).ToList());
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _tasksService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskItem = new TodoItemWithAllLists
            {
                TaskLists = await _tasksService.GetAllTaskLists(),
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                TodoList = task.TodoList,
                Status = task.Status
            };

            return View(taskItem);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,TodoListId,Status")] TodoItemWithAllLists task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingTask = await _tasksService.GetTaskById(id);

                    if (existingTask == null)
                    {
                        return NotFound();
                    }

                    task.CreatedDate = existingTask.CreatedDate;

                    await _tasksService.UpdateTask(task);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return View(task);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _tasksService.GetTaskById(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost("toggle/{id}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var task = await _tasksService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Status = task.Status == TaskItemStatus.Active ? TaskItemStatus.Completed : TaskItemStatus.Active;
            await _tasksService.UpdateTask(task);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _tasksService.GetTaskById(id);

            await _tasksService.DeleteTask(task);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpPost("delete-completed")]
        public async Task<IActionResult> DeleteCompleted()
        {
            await _tasksService.DeleteCompletedTasks();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
