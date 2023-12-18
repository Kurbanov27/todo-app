using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo_aspnetmvc_ui.Interfaces;
using todo_aspnetmvc_ui.Services;
using todo_domain_entities.Models;

namespace todo_aspnetmvc_ui.Controllers
{
    [Route("[controller]")]
    public class ListController : Controller
    {
        private readonly IListsService _listService;

        public ListController(IListsService listService)
        {
            _listService = listService;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var todoList = new TaskList();

            return View(todoList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Title")] TaskList todoList)
        {
            if (ModelState.IsValid)
            {
                await _listService.CreateTaskList(todoList);
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return View(todoList);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _listService.GetTaskListById(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var list = await _listService.GetTaskListById(id);

            await _listService.DeleteTaskList(list);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var list = await _listService.GetTaskListById(id);

            if (list == null)
            {
                return NotFound();
            }

            var listItem = new TaskList
            {
                Id = list.Id,
                Title = list.Title
            };

            return View(listItem);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] TaskList list)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _listService.UpdateTaskList(list);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return View(list);
        }
    }
}
