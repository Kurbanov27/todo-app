using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Models;
using todo_domain_entities.Data;
using todo_domain_entities.Enums;

namespace todo_aspnetmvc_ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoContext _context;

        public HomeController(TodoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.TaskLists.Include(x => x.TodoItems).ToListAsync();

            ViewBag.ActiveTasksCount = result.SelectMany(l => l.TodoItems).Count(t => t.Status == TaskItemStatus.Active);

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
