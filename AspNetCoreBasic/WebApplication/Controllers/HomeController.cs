using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLibrary;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context
                .Blogs
                // we don't need EF's change tracker to start tracking these entities
                // so we can save a little overhead by turning off the change tracker for this query
                .AsNoTracking() 
                .ToListAsync();

            return View(model);
        }
    }
}